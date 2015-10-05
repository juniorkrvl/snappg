using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snappg.Core
{
    public class Config
    {
        public Database Database { get; set; }
        public Entities Entities { get; set; }
        public Pocos Pocos { get; set; }
        public string Root { get; set; }
    }

    public class Database
    {
        public string Server { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Provider { get; set; }
    }

    public class Entities
    {
        public string Path { get; set; }
        public string Namespace { get; set; }
        public string Project { get; set; }
        public string[] Ignores { get; set; }
    }

    public class Pocos
    {
        public string[] Imports { get; set; }
        public string Sulfix { get; set; }
        public string Prefix { get; set; }
        public Attributes Attributes { get; set; }
        public Relations Relations { get; set; }
    }

    public class Attributes
    {
        public string[] PK { get; set; }
        public string[] FK { get; set; }
        public string[] Table { get; set; }
        public string[] Columns { get; set; }
    }

    public class Relations
    {
        public bool? OneToOne { get; set; }
        public bool? OneToMany { get; set; }
    }
}
