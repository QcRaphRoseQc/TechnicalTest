using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using Quartz;
using TechnicalTest.API.Controllers;
using TechnicalTest.API.Data;
using TechnicalTest.API.Models;
using static TechnicalTest.API.Startup;

namespace TechnicalTest.API.Quartz;

public class EmailJob : IJob
{
    private readonly ILogger<EmailJob> m_logger;
    private String CsvPath { get; set; }

    public EmailJob(ILogger<EmailJob> logger)
    {
        m_logger = logger;
    }

    public async Task Export()
    {
        string db_setting = ("Host=postgres.data;Database=TechnicalTestDb;Username=postgres;Password=tfamFuRXWCnyik6Vnh9x");
        NpgsqlConnection conn = new NpgsqlConnection(db_setting);
        conn.Open();

        string query = "SELECT * FROM \"Events\" WHERE \"DeclarationDateTime\" >= CURRENT_DATE AND \"DeclarationDateTime\" <= CURRENT_DATE + INTERVAL '1 day'";

        NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
        NpgsqlDataReader dr = cmd.ExecuteReader();
        var csv = new StringBuilder();
        csv.AppendLine("Id,Description,DeclarationDateTime,DeclaredById");
        while (dr.Read())
        {
            var newLine = string.Format("{0},{1},{2},{3}", dr["Id"], dr["Description"], dr["DeclarationDateTime"], dr["DeclaredById"]);
            csv.AppendLine(newLine);
        }
        CsvPath= System.IO.Path.Combine("csv", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".csv");
        System.IO.File.WriteAllText(CsvPath, csv.ToString());
        conn.Close();

    }

    public async Task Execute(IJobExecutionContext context)
    {
        await Export();
        using (var msg = new MailMessage("raphael.b.larose@gmail.com", "navysonar2002@gmail.com"))
        {
            msg.Attachments.Add(new Attachment(CsvPath));
            msg.Subject = DateTime.Now.ToString("yyyy-MM-dd") + " - Event Report";
            msg.Body = "Please find attached the event report for " + DateTime.Now.ToString("yyyy-MM-dd");
            using (SmtpClient sc = new SmtpClient())
            {
                sc.EnableSsl = true;
                sc.Host = "smtp.gmail.com";
                sc.Port = 587;
                sc.Credentials = new NetworkCredential("raphael.b.larose@gmail.com", "exhqcuwhhbouvzoz");
                sc.UseDefaultCredentials = false;
                sc.Send(msg);
                m_logger.LogInformation("Email sent successfully");
            }
        }
    }
}