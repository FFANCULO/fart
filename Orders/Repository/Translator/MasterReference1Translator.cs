using Npgsql;

namespace Legislative.Repository.Translator
{
    public record MasterReference1Translator : INpgsqlNameTranslator
    {
        /// <summary>
        /// Given a CLR type name (e.g class, struct, enum), translates its name to a database type name.
        /// </summary>
        public string TranslateTypeName(string clrName)
        {
            return clrName;
        }

        /// <summary>
        /// Given a CLR member name (property or field), translates its name to a database type name.
        /// </summary>
        public string TranslateMemberName(string clrName)
        {
            return clrName;
        }
    }
}