namespace API.Models
{
	public class MemberAccountRelation
	{
		public int MemberId { get; set; }
		public MemberModel Member { get; set; }

		public int AccountId { get; set; }
		public AccountModel Account { get; set; }
	}
}
