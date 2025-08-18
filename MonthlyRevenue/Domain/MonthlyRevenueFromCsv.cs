using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonthlyRevenue.Domain
{
    public class MonthlyRevenueFromCsv
    {
        public int? ReportDate { get; set; }

        [Required]
        public int DataYearMonth { get; set; }

        [Required]
        public int CompanyCode { get; set; }

        [MaxLength(50)]
        public string? CompanyName { get; set; }

        [MaxLength(50)]
        public string? Industry { get; set; }

        public long? Rev_CurrentMonth { get; set; }
        public long? Rev_PreviousMonth { get; set; }
        public long? Rev_SameMonthLastYear { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? MoM_ChangePct { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? YoY_ChangePct { get; set; }

        public long? Rev_Accu_CurrentYear { get; set; }
        public long? Rev_Accu_LastYear { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? Accu_YoY_ChangePct { get; set; }
        public string? Notes { get; set; } = string.Empty;
        public int? TotalCount { get; set; }
    }

}
