using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Models;
using 記帳APP.Models.DTOs;
using 記帳APP.Repository;
using 記帳APP.Utility;
using static 記帳APP.Contract.AccountBookContract;

namespace 記帳APP.Presenter
{
    internal class AccountBookPresenter : IAccountBookPresenter

    {
        IAccountBookView accountBookView;
        public AccountBookPresenter(IAccountBookView accountBookView)
        {
            this.accountBookView = accountBookView;
        }
        public void GetRecord(DateTime fromDate, DateTime toDate)
        {
            RecordRepository recordRepository = new RecordRepository();
            List<RecordDAO> recordDAOs = recordRepository.GetRecords(fromDate, toDate);
            List<AccountBookDTO> accountBookDTOs = new List<AccountBookDTO>();
            for (int i = 0; i < recordDAOs.Count; i++)
            {
                AccountBookDTO accountBookDTO = new AccountBookDTO();
                accountBookDTO.Date = recordDAOs[i].Date;
                accountBookDTO.Category = recordDAOs[i].Category;
                accountBookDTO.Subcategory = recordDAOs[i].Subcategory;
                accountBookDTO.Price = recordDAOs[i].Price;
                accountBookDTO.Target = recordDAOs[i].Target;
                accountBookDTO.Payment = recordDAOs[i].Payment;
                accountBookDTO.Picture1 = recordDAOs[i].Picture1;
                accountBookDTO.Picture2 = recordDAOs[i].Picture2;
                accountBookDTOs.Add(accountBookDTO);
            }
            accountBookView.GetRecordResponse(accountBookDTOs);
        }

        public void DeleteRecord(AccountBookDTO accountBookDTO)
        {
            RecordDAO recordDAO = new RecordDAO();
            recordDAO.Date = accountBookDTO.Date;
            recordDAO.Category = accountBookDTO.Category;
            recordDAO.Subcategory = accountBookDTO.Subcategory;
            recordDAO.Price = accountBookDTO.Price;
            recordDAO.Target = accountBookDTO.Target;
            recordDAO.Payment = accountBookDTO.Payment;
            recordDAO.Picture1 = accountBookDTO.Picture1;
            recordDAO.Picture2 = accountBookDTO.Picture2;

            File.Delete(accountBookDTO.Picture1);
            File.Delete(accountBookDTO.Picture2);
            File.Delete(accountBookDTO.Picture1.Replace("small_", ""));
            File.Delete(accountBookDTO.Picture2.Replace("small_", ""));
            //刪照片
            RecordRepository recordRepository = new RecordRepository();
            recordRepository.DeleteRecord(recordDAO);

        }



        public void UpdateRecord(AccountBookDTO accountBookDTO)
        {
            RecordDAO recordDAO = new RecordDAO();
            recordDAO.Date = accountBookDTO.Date;
            recordDAO.Category = accountBookDTO.Category;
            recordDAO.Subcategory = accountBookDTO.Subcategory;
            recordDAO.Price = accountBookDTO.Price;
            recordDAO.Target = accountBookDTO.Target;
            recordDAO.Payment = accountBookDTO.Payment;
            recordDAO.Picture1 = accountBookDTO.Picture1;
            recordDAO.Picture2 = accountBookDTO.Picture2;

            RecordRepository recordRepository = new RecordRepository();
            recordRepository.UpdateRecord(recordDAO);

        }
    }
}
