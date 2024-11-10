namespace CarpetCleaning.Web.Models.ManageUser
{
    public class AddOrRemoveClaimModel
    {
        #region Constructor

        public AddOrRemoveClaimModel()
        {
            UserClaims = new List<ClaimsModel>();
        }

        public AddOrRemoveClaimModel(string userId, IList<ClaimsModel> userClaims)
        {
            UserId = userId;
            UserClaims = userClaims;
        }

        #endregion


        public string UserId { get; set; }
        public IList<ClaimsModel> UserClaims { get; set; }
    }

    public class ClaimsModel
    {

        #region Constructor

        public ClaimsModel()
        {
        }

        public ClaimsModel(string claimType, string persionClaimType)
        {
            ClaimType = claimType;
            PersionClaimType = persionClaimType;
        }

        #endregion

        public string ClaimType { get; set; }
        public string PersionClaimType { get; set; }
        public bool IsSelected { get; set; }
    }
}
