using System.Collections.Generic;
using KaliGasService.Core.Types;

namespace KaliGasService.Core.Data
{

    public abstract class BasicEnumConverter<TCodeType, TEnumType>
    {
        private readonly BiDictionary<TCodeType, TEnumType> _codeToEnum;

        protected BasicEnumConverter(BiDictionary<TCodeType, TEnumType> codeToEnum)
        {
            _codeToEnum = codeToEnum;
        }

        public TEnumType CodeToEnum(TCodeType code)
        {
            return _codeToEnum.Forward[code];
        }

        public TCodeType EnumToCode(TEnumType @enum)
        {
            return _codeToEnum.Reverse[@enum];
        }

        public IEnumerable<TCodeType> GetAllCodes()
        {
            return _codeToEnum.GetKeys();
        }

        public IEnumerable<TEnumType> GetAllEnumTypes()
        {
            return _codeToEnum.GetValues();
        }
    }
}
