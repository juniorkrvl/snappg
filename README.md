### Introduction
When we work with some ORM's, we need objects that represent our database within the project. The Snappg was born in order to simplify the generation of these files known as Plain Old CLR Object.

### Features
Snappg is a .exe program that uses one simple config file to generate your pocos and include them automatically into your CSharp project.

### Getting Started
First of all, you need to install Snappg in your Windows.
You can download a package here or clone the repository and build for your own. After install, you'll need to put the path of snappg.exe into windows environment variables. (If you install from package installer, you get it automatically). 

After install and configure, you are ready to go!

### Snappg Commands

#### Snappg Init

The first command you will need to use is init. Go to your main project folder and type:

    snappg init

This command will create a **snappg-config.json** file into your current directory.

#### Snappg Update

After snappg-config.json file be ready and properly configured, type:

    snappg update

To create/update your pocos.

### Snappg Configuration

This **snappg-config.json** file is the core of snappg. Here you will be able to configure every detail of the generation of your project POCO files.

```json
{
  "database": {
    "server": "127.0.0.1",
    "name": "database_name",
    "user": "database_user",
    "password": "database_password",
    "provider": "MySql.Data.MySqlClient"
  },
  "entities": {
    "path": "C:\\Projects\\ExampleProj\\Entities",
    "namespace": "ExampleProj.Entities",
    "project": "C:\\Projects\\ExampleProj\\Entities\\ExampleProj.csproj",
    "ignores": [
      "User",
      "Products"
    ]
  },
  "pocos": {
    "imports": ["System","System.Collections.Generic"],
    "sulfix": "Poco",
    "prefix": "Tb",
    "attributes": {
      "pk": "[Key]",
      "fk": "",
      "table": ["[TableName(\"@name@\")]"],
      "columns": "",
      "oneToOneRelations": "",
      "oneToManyRelations": ""
    },
    "relations": {
      "oneToOne": true,
      "oneToMany": true
    }
  }

}
```

#### Database Configuration

```json
"database": {
    "server": "127.0.0.1",
    "name": "database_name",
    "user": "database_user",
    "password": "database_password",
    "provider": "MySql.Data.MySqlClient"
  }
```

In this block, you will configure all database informations. For provider option, you can choose between 'MySql.Data.MySqlClient' for MySql or 'System.Data.SqlClient' for SQL Server for now.

#### Entities Configuration

```json
 "entities": {
    "path": "C:\\Projects\\ExampleProj\\Entities",
    "namespace": "ExampleProj.Entities",
    "project": "C:\\Projects\\ExampleProj\\Entities\\ExampleProj.csproj",
    "ignores": [
      "User",
      "Products"
    ]
  }
```

**Path**: Path to save your generated files.

**Namespace**: Namespace of genereted classes.

**Project**: Path to .csproj file. This option is for inserting files into your project file automatically. With this, you will not need to add manually through the menu > 'Include In Project'.

**Ignores**: Here you place an array of string with the names of the tables that will not use. Snappg will ignore these tables and if you have previously generated, they will be erased.

#### Pocos Configuration

```json
"pocos": {
    "imports": ["System","System.Collections.Generic","System.ComponentModel.DataAnnotations"],
    "sulfix": "Poco",
    "prefix": "Tb",
    "attributes": {
      "pk": ["[Key]"],
      "fk": ["[ForeignKey]"],
      "table": ["[Table]"],
      "columns": ["[Column]"]
    },
    "relations": {
      "oneToOne": true,
      "oneToMany": true
    }
  }
```

Here you put the details of your files.

**Imports**: Place all imports your project needs.
**Sulfix**: Place a sulfix to table name into POCO class name. Example: Table-> User, Class-> UserPoco
**Prefix**: Place a prefix to table name into POCO class name. Example: Table-> User, Class-> TbUser

##### Attributes Configuration

**PK**: Here you place an array of strings that indicates the attributes that you want the properties representing PrimaryKeys should have.

Example:
```c#
public class User{
    [Key]
    public int Id { get; set;}
    public string Name { get; set;}
}
```

**FK**: Here you place an array of strings that indicates the attributes that you want the properties representing ForeignKeys should have.

Example:
```c#
public class ProductItem{
    [Key]
    public int Id { get; set;}
    [ForeignKey]
    public int ProductId { get; set;}
    public string Name { get; set;}
}
```

**Columns**: Here you place an array of strings that indicates the attributes that all properties should have.

Example:
```c#
public class ProductItem{
    [Key]
    [Column]
    public int Id { get; set;}
    [ForeignKey]
    [Column]
    public int ProductId { get; set;}
    [Column]
    public string Name { get; set;}
}
```

##### Relations Configuration

```json
"relations": {
      "oneToOne": true,
      "oneToMany": true
    }
```

Here you define if your pocos will have references to one-to-one classes, or one-to-many classes

**oneToOne**: Set true if you want one to one relationships are referenced in the class.

Example

```c#
public class User {
    public int Id { get;set;}
    public string Name {get;set;}

    public Address Address {get;set;} //-> One to one relationship
}
```

**oneToMany**: Set true if you want one to many relationships are referenced in the class as a list.

Example

```c#
public class User {
    public int Id {get;set;}
    public string Name {get;set;}

    public List<Phone> Phone {get;set;} //-> One to many relationship
}
```

### Authors and Contributors
Snappg was designed by @juniorkrvl in order to simplify the generation of POCO's for any kind of project.
