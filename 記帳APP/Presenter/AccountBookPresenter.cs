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
            List<AccountBookDTO> accountBookDTOs = Mapper.Map<RecordDAO, AccountBookDTO>(recordDAOs);

            accountBookView.GetRecordResponse(accountBookDTOs);
        }

        public void DeleteRecord(AccountBookDTO accountBookDTO)
        {
            RecordDAO recordDAO = Mapper.Map<AccountBookDTO, RecordDAO>(accountBookDTO);


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
            RecordDAO recordDAO = Mapper.Map<AccountBookDTO, RecordDAO>(accountBookDTO);


            RecordRepository recordRepository = new RecordRepository();
            recordRepository.UpdateRecord(recordDAO);

        }
    }
}
