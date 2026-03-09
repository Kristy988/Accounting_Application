using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Models.DTOs;

namespace 記帳APP.Contract
{
    internal class AddRecordContract
    {//記一筆
        internal interface IAddRecordView
        {
            void GetComboBoxDataResponse(DataDTO dataDTO);
            void GetSubcatComboBoxResponse(List<string> Subcates);
        }
        internal interface IAddRecordPresenter
        {
            void CreateRecord(RecordDTO recordDTO);
            void ChangeSubcatComboBox(string categoryItem);
            void GetComboBoxData();


        }
    }
}
