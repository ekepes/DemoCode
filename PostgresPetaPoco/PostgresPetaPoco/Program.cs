using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgresPetaPoco
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database("Postgres");

            // Show all articles    
            foreach (var member in db.Query<Members>("SELECT * FROM cd.members"))
            {
                Console.WriteLine("{0} - {1}, {2}", 
                    member.MemberId, 
                    member.Surname, 
                    member.FirstName);
            }

            db.Dispose();
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    [PetaPoco.TableName("cd.members")]
    [PetaPoco.PrimaryKey("memid")]
    public class Members
    {
        [Column("memid")]
        // memid integer NOT NULL,
        public int MemberId { get; set; }

        //surname character varying(200) NOT NULL,
        public string Surname { get; set; }
        
        //firstname character varying(200) NOT NULL,
        public string FirstName { get; set; }
        
        //address character varying(300) NOT NULL,
        public string Address { get; set; }
        
        //zipcode integer NOT NULL,
        public int ZipCode { get; set; }
        
        //telephone character varying(20) NOT NULL,
        public string Telephone { get; set; }
        
        //recommendedby integer,
        public int RecommendedBy { get; set; }
        
        //joindate timestamp without time zone NOT NULL,
        public DateTime JoinDate { get; set; }
    }
}
