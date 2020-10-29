namespace VolvoCash.Application.MainContext.DTO.Admins
{
    public class AdminDTO
    {
        #region Properties
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int UserId { get; set; }
        #endregion
    }
}
