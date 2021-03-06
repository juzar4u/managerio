




















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `ManagerIO`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=.;Initial Catalog=ManagerIO;User ID=sa;Password=juz95934600`
//     Schema:                 ``
//     Include Views:          `False`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace ManagerIO.DataModels
{

	public partial class ManagerIORepository : Database
	{
		public ManagerIORepository() 
			: base("ManagerIO")
		{
			CommonConstruct();
		}

		public ManagerIORepository(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			ManagerIORepository GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static ManagerIORepository GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new ManagerIORepository();
        }

		[ThreadStatic] static ManagerIORepository _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

		public class Record<T> where T:new()
		{
			public static ManagerIORepository repo { get { return ManagerIORepository.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }

			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }

			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

		}

	}
	



    

	[TableName("dbo.Accounts")]



	[PrimaryKey("AccountID")]




	[ExplicitColumns]

    public partial class Account : ManagerIORepository.Record<Account>  
    {



		[Column] public int AccountID { get; set; }





		[Column] public string AccountKey { get; set; }





		[Column] public string AccountName { get; set; }





		[Column] public int AccountType { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }





		[Column] public decimal? CreditLimit { get; set; }





		[Column] public string Code { get; set; }



	}

    

	[TableName("dbo.Attachment")]



	[PrimaryKey("AttachmentID")]




	[ExplicitColumns]

    public partial class Attachment : ManagerIORepository.Record<Attachment>  
    {



		[Column] public int AttachmentID { get; set; }





		[Column] public string AttachmentKey { get; set; }





		[Column] public DateTime? AttachmentDate { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string ContentType { get; set; }





		[Column] public decimal? Size { get; set; }





		[Column] public string InnerObjectKey { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }





		[Column] public string AttachmentUrl { get; set; }



	}

    

	[TableName("dbo.Business")]



	[PrimaryKey("BusinessID")]




	[ExplicitColumns]

    public partial class Business : ManagerIORepository.Record<Business>  
    {



		[Column] public decimal BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public string BusinessName { get; set; }





		[Column] public bool IsActive { get; set; }





		[Column] public string BaseApiUrl { get; set; }



	}

    

	[TableName("dbo.BusinessDetail")]



	[PrimaryKey("BusinessDetailID")]




	[ExplicitColumns]

    public partial class BusinessDetail : ManagerIORepository.Record<BusinessDetail>  
    {



		[Column] public int BusinessDetailID { get; set; }





		[Column] public string BusinessDetailKey { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Address { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }



	}

    

	[TableName("dbo.BusinessLogo")]



	[PrimaryKey("BusinessLogoID")]




	[ExplicitColumns]

    public partial class BusinessLogo : ManagerIORepository.Record<BusinessLogo>  
    {



		[Column] public int BusinessLogoID { get; set; }





		[Column] public string BusinessLogoKey { get; set; }





		[Column] public string ContentType { get; set; }





		[Column] public string Content { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }



	}

    

	[TableName("dbo.ChartofAccounts")]



	[PrimaryKey("AccountID")]




	[ExplicitColumns]

    public partial class ChartofAccount : ManagerIORepository.Record<ChartofAccount>  
    {



		[Column] public int AccountID { get; set; }





		[Column] public string AccountKey { get; set; }





		[Column] public string AccountName { get; set; }





		[Column] public int? ParentAccountID { get; set; }





		[Column] public string ParentAccountKey { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }





		[Column] public int? Position { get; set; }



	}

    

	[TableName("dbo.Component")]



	[PrimaryKey("ComponentID")]




	[ExplicitColumns]

    public partial class Component : ManagerIORepository.Record<Component>  
    {



		[Column] public int ComponentID { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Rate { get; set; }





		[Column] public string Account { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }





		[Column] public int? TaxCodeID { get; set; }



	}

    

	[TableName("dbo.Customers")]



	[PrimaryKey("CustomerID")]




	[ExplicitColumns]

    public partial class Customer : ManagerIORepository.Record<Customer>  
    {



		[Column] public int CustomerID { get; set; }





		[Column] public string CustomerKey { get; set; }





		[Column] public string CustomerName { get; set; }





		[Column] public string CustomerGSTID { get; set; }





		[Column] public string CustomerAddress { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }





		[Column] public string Email { get; set; }





		[Column] public string Code { get; set; }





		[Column] public decimal? CreditLimit { get; set; }



	}

    

	[TableName("dbo.Description")]



	[PrimaryKey("DescriptionID")]




	[ExplicitColumns]

    public partial class Description : ManagerIORepository.Record<Description>  
    {



		[Column] public int DescriptionID { get; set; }





		[Column("Description")] public string _Description { get; set; }





		[Column] public string Qty { get; set; }





		[Column] public string Item { get; set; }





		[Column] public decimal? Amount { get; set; }





		[Column] public string Account { get; set; }





		[Column] public string TaxCode { get; set; }





		[Column] public bool IsActive { get; set; }





		[Column] public decimal? Discount { get; set; }





		[Column] public int? DiscountType { get; set; }





		[Column] public int? EntityID { get; set; }





		[Column] public int? EntityTypeID { get; set; }





		[Column] public string InvoiceKey { get; set; }





		[Column] public int? Sequence { get; set; }



	}

    

	[TableName("dbo.Folders")]



	[PrimaryKey("FolderID")]




	[ExplicitColumns]

    public partial class Folder : ManagerIORepository.Record<Folder>  
    {



		[Column] public int FolderID { get; set; }





		[Column] public string FolderKey { get; set; }





		[Column] public string FolderName { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }



	}

    

	[TableName("dbo.InnerObject")]



	[PrimaryKey("InnerObjectID")]




	[ExplicitColumns]

    public partial class InnerObject : ManagerIORepository.Record<InnerObject>  
    {



		[Column] public int InnerObjectID { get; set; }





		[Column] public string InnerObjectKey { get; set; }





		[Column] public string InnerObjectName { get; set; }





		[Column] public int? ParentObjectID { get; set; }





		[Column] public string ParentObjectKey { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }



	}

    

	[TableName("dbo.InventoryItem")]



	[PrimaryKey("InventoryID")]




	[ExplicitColumns]

    public partial class InventoryItem : ManagerIORepository.Record<InventoryItem>  
    {



		[Column] public int InventoryID { get; set; }





		[Column] public string InventoryKey { get; set; }





		[Column] public string InventoryName { get; set; }





		[Column] public string Description { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string UnitName { get; set; }





		[Column] public decimal? PurchasePrice { get; set; }





		[Column] public decimal? SalePrice { get; set; }





		[Column] public string TaxCodeKey { get; set; }





		[Column] public string IncomeAccountKey { get; set; }





		[Column] public bool? IsInactiveInApi { get; set; }





		[Column] public string ExpenseAccountKey { get; set; }



	}

    

	[TableName("dbo.Invoice")]



	[PrimaryKey("InvoiceID")]




	[ExplicitColumns]

    public partial class Invoice : ManagerIORepository.Record<Invoice>  
    {



		[Column] public int InvoiceID { get; set; }





		[Column] public string InvoiceKey { get; set; }





		[Column] public string InvoiceBillNo { get; set; }





		[Column] public DateTime? IssueDate { get; set; }





		[Column] public string PartyKey { get; set; }





		[Column] public string InvoiceSummary { get; set; }





		[Column] public int? RoundingMethod { get; set; }





		[Column] public bool? IsRounding { get; set; }





		[Column] public int? InvoiceType { get; set; }





		[Column] public int? PartyType { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }





		[Column] public bool? IsDiscount { get; set; }





		[Column] public int? DiscountType { get; set; }





		[Column] public bool? IsAmountsIncludeTax { get; set; }





		[Column] public string PurchaseOrderNo { get; set; }





		[Column] public string SalesQuoteNo { get; set; }





		[Column] public string Address { get; set; }



	}

    

	[TableName("dbo.Object")]



	[PrimaryKey("ObjectID")]




	[ExplicitColumns]

    public partial class Object : ManagerIORepository.Record<Object>  
    {



		[Column] public int ObjectID { get; set; }





		[Column] public string ObjectKey { get; set; }





		[Column] public string ObjectName { get; set; }





		[Column] public bool IsActive { get; set; }





		[Column] public int? OrderBy { get; set; }



	}

    

	[TableName("dbo.ReceiptsPayments")]



	[PrimaryKey("ReceiptPaymentID")]




	[ExplicitColumns]

    public partial class ReceiptsPayment : ManagerIORepository.Record<ReceiptsPayment>  
    {



		[Column] public int ReceiptPaymentID { get; set; }





		[Column] public string ReceiptPaymentKey { get; set; }





		[Column] public DateTime? ReceiptPaymentDate { get; set; }





		[Column] public string AccountKey { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Payee { get; set; }





		[Column] public int? TransectionType { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }





		[Column] public bool? IsAmountsIncludeTax { get; set; }





		[Column] public string Reference { get; set; }





		[Column] public bool? IsBalanceClear { get; set; }



	}

    

	[TableName("dbo.Suppliers")]



	[PrimaryKey("SupplierID")]




	[ExplicitColumns]

    public partial class Supplier : ManagerIORepository.Record<Supplier>  
    {



		[Column] public int SupplierID { get; set; }





		[Column] public string SupplierKey { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Email { get; set; }





		[Column] public string Address { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }





		[Column] public string Code { get; set; }





		[Column] public decimal? CreditLimit { get; set; }



	}

    

	[TableName("dbo.sysdiagrams")]



	[PrimaryKey("diagram_id")]




	[ExplicitColumns]

    public partial class sysdiagram : ManagerIORepository.Record<sysdiagram>  
    {



		[Column] public string name { get; set; }





		[Column] public int principal_id { get; set; }





		[Column] public int diagram_id { get; set; }





		[Column] public int? version { get; set; }





		[Column] public byte[] definition { get; set; }



	}

    

	[TableName("dbo.TaxCodes")]



	[PrimaryKey("TaxCodeID")]




	[ExplicitColumns]

    public partial class TaxCode : ManagerIORepository.Record<TaxCode>  
    {



		[Column] public int TaxCodeID { get; set; }





		[Column] public string ComponentKey { get; set; }





		[Column] public string TaxCodeName { get; set; }





		[Column] public string TaxRate { get; set; }





		[Column] public string TaxRateType { get; set; }





		[Column] public string Account { get; set; }





		[Column] public string SalesInvoiceTitle { get; set; }





		[Column] public bool? CustomSalesInvoiceTitle { get; set; }





		[Column] public decimal? BusinessID { get; set; }





		[Column] public string BusinessKey { get; set; }





		[Column] public bool IsActive { get; set; }



	}


}
