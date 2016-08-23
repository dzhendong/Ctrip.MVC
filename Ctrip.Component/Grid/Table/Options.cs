using System;
using System.Collections;
using System.Collections.Generic;

namespace Ctrip.Component
{
    public class Options
    {
        #region Property

        private Int32 _rowNum = 0;
        public Int32 RowNum
        {
            get { return _rowNum; }
            set { _rowNum = value; }
        }

        private string _tableID = "TableID";
        public string TableID
        {
            get { return _tableID; }
            set { _tableID = value; }
        }

        private string _tableName = string.Empty;
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        private TableStyle _currenTableStyle = new TableStyle();
        public TableStyle CurrenTableStyle
        {
            get { return _currenTableStyle; }
            set { _currenTableStyle = value; }
        }

        public Int32 customizedStyleCount = 0;
        Dictionary<string, string> _customizedStyleSet = new Dictionary<string, string>();
        public IEnumerable GetCustomizedStyle
        {
            get
            {

                foreach (KeyValuePair<string, string> item in _customizedStyleSet)
                {

                    yield return new StyleParameter { Name = item.Key, Value = item.Value };
                }

            }
        }

        public Int32 customizedClassCount = 0;
        Dictionary<string, string> _customizedClassSet = new Dictionary<string, string>();
        public IEnumerable GetCustomizedClass
        {
            get
            {

                foreach (KeyValuePair<string, string> item in _customizedClassSet)
                {
                    yield return new StyleParameter { Name = item.Key, Value = item.Value };
                }

            }
        }

        #endregion

        #region Methods

        public void SetStyle(EnumSetSytleType setType, Dictionary<string, string> setStyleDic)
        {
            if (setStyleDic == null || setStyleDic.Count == 0)
            {
                return;
            }
            foreach (KeyValuePair<string, string> item in setStyleDic)
            {
                if (!string.IsNullOrEmpty(_tableID) && !string.IsNullOrEmpty(item.Value))
                {
                    switch (setType)
                    {
                        case EnumSetSytleType.Style:
                            customizedStyleCount++;
                            if (!_customizedStyleSet.ContainsKey("td_" + _tableID + "_" + item.Key))
                            {
                                _customizedStyleSet.Add("td_" + _tableID + "_" + item.Key, item.Value);
                            }
                            break;
                        case EnumSetSytleType.Class:
                            customizedClassCount++;
                            if (!_customizedClassSet.ContainsKey("td_" + _tableID + "_" + item.Key))
                            {
                                _customizedClassSet.Add("td_" + _tableID + "_" + item.Key, item.Value);
                            }
                            break;
                    }
                }
            }

        }
        #endregion
    }
}
