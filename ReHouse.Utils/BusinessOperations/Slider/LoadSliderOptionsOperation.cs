using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Common;

namespace ReHouse.Utils.BusinessOperations.Slider
{
    public class LoadSliderOptionsOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<SliderParam> _params { get; set; }

        public LoadSliderOptionsOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Получение всех слайдов";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _params = Context.SliderParams.Where(x => !x.Deleted).ToList();
        }
    }
}