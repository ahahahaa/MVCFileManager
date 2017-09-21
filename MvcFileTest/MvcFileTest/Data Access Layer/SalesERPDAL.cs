using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MvcFileTest.Models;

namespace MvcFileTest.Data_Access_Layer
{
    public class SalesERPDAL : DbContext
    {
        //修改connectionstring name，与dal类名相同，实现映射
        public SalesERPDAL()
            : base("EmployeeSalaryDB")
        { }

        //定义映射关系
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("TblEmployee");
            base.OnModelCreating(modelBuilder);
        }

        //DbSet: db中可查询的实体集合，调用Employees，获取table中所有记录，转换成Employee对象，返回对象集合
        public DbSet<Employee> Employees { get; set; }

    }

    

}