namespace Product_management.ModelsTest
{
    public class Serv : ISer1,IS3,IS2
    {
       Guid Id { get; set; }


        public Serv()
        {
            Id = Guid.NewGuid();
        }
       public Guid getRandomNumber()
        {
            
            return Id;
        }

    }
}
