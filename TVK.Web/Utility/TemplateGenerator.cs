using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TVK.Web.Utility
{
    public class TemplateGenerator
    {
        private static testSQLContext db = new testSQLContext();

        public static string GetHTMLStringUsers()
        {

            List<Users> users = db.Users.ToList();

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Список пользователей</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Ник</th>
                                        <th>Регистрационный Email</th>
                                        <th>Фамилия</th>
                                        <th>Имя</th>
                                        <th>Отчество</th>
                                        <th>Пол</th>
                                        <th>Возраст</th>
                                        <th>Роль</th>
                                        <th>Телефон или Email</th>
                                        <th>Комментарий</th>
                                    </tr>");

            foreach (var user in users)
            {
                PersonalInformation personal = db.PersonalInformation
                    .Where(t => t.IdUserPersonalInformation == user.IdUser)
                    .FirstOrDefault();
               
                Roles role = db.Roles
                    .Where(t => t.IdUsersRole == user.IdUser)
                    .FirstOrDefault();

                BackgroundRoles backrole = db.BackgroundRoles
                    .Where(t => t.IdBackgroundRole == role.IdBackgroundrole)
                    .FirstOrDefault();


                ContactInformation contact = db.ContactInformation
                    .Where(t => t.IdUserContactInformation == user.IdUser)
                    .FirstOrDefault();
                    
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>
                                    <td>{7}</td>
                                    <td>{8}</td>
                                    <td>{9}</td>
                                  </tr>", user.Nameusers, user.Email, personal.Lastname, personal.Firstname, personal.Secondname, personal.Gender, 
                                  personal.Age, backrole.DescriptionRole, contact.PhoneOrEmail, contact.Comment);
            }


            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }

        public static string GetHTMLStringComand()
        {
            var backgroundCommand = db.BackgroundCommand.ToList();

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Список команд</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Команда</th>
                                        <th>Описание команды</th>

                                    </tr>");

            foreach (var backcomand in backgroundCommand)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                  </tr>", backcomand.Command, backcomand.Help);
            }


            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }

        public static string GetHTMLStringSysteminfo()
        {
            var pclist = db.Pc.ToList();


            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Список компьютеров</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Имя компьютера, IP-адрес, Дата последней проверки</th>
                                        <th>Имя ОС, Изготовитель системы</th>
                                        <th>Модель системы, Тип системы</th>
                                        <th>Опертивная память</th>
                                        <th>Сетевые адаптеры</th> 
                                    </tr>");

            foreach (var pc in pclist)
            {

                HistorySysteminfo historySysteminfo = db.HistorySysteminfo
                        .Where(t => t.IdPcHistorySysteminfo == pc.IdPc)
                        .LastOrDefault();

                Systeminfo systeminfo = db.Systeminfo
                        .Where(t => t.IdSysteminfo == historySysteminfo.IdHistorySysteminfo)
                        .LastOrDefault();
                string info = pc.NamePc + " " + pc.IpAddress + " " + historySysteminfo.DateHistory;
                string info1 = systeminfo.NameOfOs + " " + systeminfo.SystemManufacturer;
                string info2 = systeminfo.ModelSystem + " " + systeminfo.TypeOfSystem;

                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>

                                  </tr>", info, info1, info2, systeminfo.PhysicalMemory, systeminfo.NetworkAdapters);
            }


            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }

        public static string GetHTMLStringPCCommand()
        {
            var pclist = db.Pc.ToList();

            



            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Компьютер и команды, выполненные на нем</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Имя компьютера</th>
                                        <th>№</th>
                                        <th>Команды, выполненные на этом компьютере</th>
                                    </tr>");
            int i = 1;
            foreach (var pc in pclist)
            {
                string commandPC = "";
                string nomer = "";
                List<Command> commands = db.Command
                        .Where(t => t.IdPcCommand == pc.IdPc)
                        .ToList();
                foreach (var command in commands)
                {
                    
                    BackgroundCommand backgroundCommand = db.BackgroundCommand
                        .Where(t => t.IdBackgroundCommand == command.IdCommandBackgroundCommand)
                        .FirstOrDefault();
                    commandPC += "Дата: " + command.Time + " " + backgroundCommand.Command + "<br>";
                    nomer += i++.ToString() + "<br>";
                }
                   
                    
                    

                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                  </tr>", pc.NamePc, nomer, commandPC);
            }


            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }


        public static string GetHTMLStringHistoryCommand()
        {
            var comandlist = db.Command.ToList();





            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>История команд</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Отправитель</th>
                                        <th>Получатель</th>
                                        <th>Команда</th>
                                        <th>Дата отправки</th>
                                    </tr>");

            foreach (var command in comandlist)
            {

                BackgroundCommand backgroundCommand = db.BackgroundCommand
                    .Where(t => t.IdBackgroundCommand == command.IdCommandBackgroundCommand)
                    .FirstOrDefault();

                Users sender = db.Users
                    .Where(t => t.IdUser == command.IdSender)
                    .FirstOrDefault();

                Users recipient = db.Users
                    .Where(t => t.IdUser == command.IdRecipient)
                    .FirstOrDefault();

                string sender_info = sender.Nameusers + " ID(" + sender.IdUser + ")";
                string recipient_info = recipient.Nameusers + " ID(" + recipient.IdUser + ")";

                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", sender_info, recipient_info, backgroundCommand.Command, command.Time);
            }


            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }


        public static string GetHTMLStringLeadTimeCommand()
        {
            var backcomandlist = db.BackgroundCommand.ToList();

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Среднее время выполнения команд</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Команда</th>
                                        <th>Общее количество выполненных команд</th>
                                        <th>Среднее время ее выполнения (секунды)</th>
                                    </tr>");

            foreach (var backcommand in backcomandlist)
            {

                List<Command> commandlist = db.Command
                    .Where(t => t.IdCommandBackgroundCommand == backcommand.IdBackgroundCommand)
                    .ToList();

               // int count_command = commandlist.Count();

                List<Data> datalist = db.Data
                    .Where(t => t.IdData == commandlist[0].IdDataCommand)
                    .ToList();
                double time = new double();

                foreach(var data in datalist)
                {
                    time += data.LeadTime.TotalSeconds;
                }

                time /= datalist.Count();

                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                  </tr>", backcommand.Command, commandlist.Count(), time);
            }


            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }


    }
}
