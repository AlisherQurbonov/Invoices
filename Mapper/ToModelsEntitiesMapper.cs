namespace invoice.Mapper;

 public static class ToModelsEntitiesMapper
    {
         
         
         public static Entities.Category ToCategoryEntities(this Models.NewCategory category )
         {
            return new Entities.Category
            
            (
                id : category.Id,
                name : category.Name
            );
         }




         public static Entities.Customer ToCustomerEntity(this Models.NewCustomer customer)
         {
             return new Entities.Customer
            (
                id : customer.Id,
                name : customer.Name,
                country : customer.Country,
                text : customer.Text,
                phone : customer.Phone

            );
         }


        public static Entities.Detail ToDetailEntity(this Models.NewDetail detail)
         {
             return new Entities.Detail
            (
                id : detail.Id,
                ord_Id : detail.Ord_Id,
                pr_Id : detail.Pr_Id,
                quantity : detail.Quantity      

            );
         }


        public static Entities.Invoice ToInvoiceEntity(this Models.NewInvoice invoice)
         {
             return new Entities.Invoice
            (
                id : invoice.Id,
                ord_Id : invoice.Ord_Id,
                amount : invoice.Amount
            );
         }


        public static Entities.Order ToOrderEntity(this Models.NewOrder order)
         {
             return new Entities.Order
            (
                id : order.Id,
                cust_id : order.Cust_Id
               
            );
         }


         public static Entities.Payment ToPaymentEntity(this Models.NewPayment paymnet)
         {
             return new Entities.Payment
            (
                id : paymnet.Id,
                amount : paymnet.Amount,
                inv_Id : paymnet.Inv_Id 
            );
         }


      
      
         public static Entities.Product ToProductEntity(this Models.NewProduct product)
         {
             return new Entities.Product
            (
                id : product.Id,
                name : product.Name,
                category_Id : product.Category_Id,
                price : product.Price,
                photo : product.Photo,
                description : product.Description
               
            );
         }


       
    }
