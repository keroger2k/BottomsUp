using BottomsUp.Core.Data;
using BottomsUp.Core.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            var db = new DatabaseContext();
            var prop = new Proposal { Name = "LA - AFB 61 CS Recompete", Created= DateTime.Now, ModifiedBy = "System", Updated = DateTime.Now };
            

            using (TextFieldParser parser = new TextFieldParser(@"c:\pws-shred.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    prop.Requirements.Add(new Requirement { 
                        PWSNumber = fields.ElementAt(0), 
                        Description = fields.ElementAt(1),
                        Updated = DateTime.Now,
                        Created = DateTime.Now,
                        ModifiedBy = "System"
                    });
                }
            }

            db.Propsals.Add(prop);
            db.SaveChanges();
            
        }
    }
}
