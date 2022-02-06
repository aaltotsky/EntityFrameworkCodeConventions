namespace EntityFrameworkCode
{
    internal class Business
    {
        DataContext context = new DataContext();
        public object GetData(string personId, string productId)
        {
            var data= from e in context.Employees
                      // I short cutted Person to pr
                      join pr in context.Persons on e.PersonId equals pr.Id
                      // I short cutted Product to p
                      join p in context.Products on e.ProductId equals p.Id
                      // Someone else put the line below thinking that the pr=Product and p is the Person
                      where pr.Id == productId && p.Id == personId // There is no simple indication that we compare apple-to-apple
                      select e;
            return data;
        }
    }
}
