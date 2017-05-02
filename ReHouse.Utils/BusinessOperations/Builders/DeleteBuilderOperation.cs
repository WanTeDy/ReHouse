using System;
using System.Data;
using System.Linq;
using System.Text;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Builders
{
    public class DeleteBuilderOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32[] _buildersId { get; set; }

        public DeleteBuilderOperation(string tokenHash, int[] buildersId)
        {
            _tokenHash = tokenHash;
            _buildersId = buildersId;
            RussianName = "Удаление застройщиков";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            if (_buildersId != null && _buildersId.Length > 0)
            {
                foreach (var builderId in _buildersId)
                {
                    var builder = Context.Builders.FirstOrDefault(x => x.Id == builderId);
                    if (builder != null)
                        Context.Builders.Remove(builder);
                }
                Context.SaveChanges();
            }
        }
    }
}