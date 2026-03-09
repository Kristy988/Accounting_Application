using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Models.DTOs;

namespace 記帳APP.Contract
{
    internal class AccountBookContract
    {
        internal interface IAccountBookView
        {
            void GetRecordResponse(List<AccountBookDTO> accountBookDTOs);
        }
        internal interface IAccountBookPresenter
        {
            void GetRecord(DateTime fromDate, DateTime toDate);
            void DeleteRecord(AccountBookDTO accountBookDTO);
            void UpdateRecord(AccountBookDTO accountBookDTO);

        }
    }
}
