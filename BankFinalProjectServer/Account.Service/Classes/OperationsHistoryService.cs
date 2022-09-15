namespace Account.Service.Classes;
public class OperationsHistoryService : IOperationsHistoryService
{
    private readonly IOperationsHistoryData _operationsHistoryData;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public OperationsHistoryService(IOperationsHistoryData operationsHistoryData, IMapper mapper, IAuthService authService)
    {
        _operationsHistoryData = operationsHistoryData;
        _mapper = mapper;
        _authService = authService;
    }

    public async Task<OperationDataListDTO> GetOperationsHistories(ClaimsPrincipal User, int pageSize, int page)
    {
        int accountID = _authService.getAccountIDFromToken(User);
        List<OperationsHistory> operationsHistories = await _operationsHistoryData.GetOperationsHistories(accountID);
        OperationDataListDTO operationsDataListDTO = new OperationDataListDTO();
        operationsDataListDTO.TotalRows = operationsHistories.Count;
        operationsHistories = operationsHistories.Skip(pageSize * page).Take(pageSize).ToList();
        List<OperationsHistoryDTO> operationsHistoriesDTO = new List<OperationsHistoryDTO>();
        foreach (var operation in operationsHistories)
        {
            OperationsHistoryDTO operationDTO = _mapper.Map<OperationsHistoryDTO>(operation);
            operationDTO.AccountNumber = await _operationsHistoryData.GetOperationAccountID(operation.TransactionID, operation.AccountID);
            operationsHistoriesDTO.Add(operationDTO);
        }
        operationsDataListDTO.Operations = operationsHistoriesDTO;
        return operationsDataListDTO;
    }
    public async Task<byte[]> CreateOperationsHistoriesPDF(int month, int year, IConverter _converter, ClaimsPrincipal User)
    {
        int accountID = _authService.getAccountIDFromToken(User);
        List<OperationsHistory> operationsHistories = await _operationsHistoryData.GetOperationsHistories(accountID);
        List<OperationsHistory> operationsHistoriesByDate = operationsHistories.Where(o => o.OperationTime.Year == year && o.OperationTime.Month == month).ToList();
        //List<OperationsHistory> operationsHistoriesByDate = this.GetOperationsHistoriesByDate(month, year, User).Result;
        var globalSettings = new GlobalSettings
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            Margins = new MarginSettings { Top = 10 },
            DocumentTitle = "Operations Histories Report PDF",
            //Out = @"C:\Users\user1\Documents\PDF\Employee_Report.pdf"
        };
        var objectSettings = new ObjectSettings
        {
            PagesCount = true,
            HtmlContent = GetHTMLString(operationsHistoriesByDate, month, year),
            WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Styles.scss") },
            HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
            FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
        };
        var pdf = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        };
        var file = _converter.Convert(pdf);
        return file;
    }

    private string GetHTMLString(List<OperationsHistory> operationsHistoriesByMonth, int month, int year)
    {
        var sb = new StringBuilder();
        sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body style='margin:40px;'>
                                <div style='text-align: center;color: darkblue; padding-bottom: 35px;' class='header'><h1>Report accounts payable for {0}/{1}</h1></div>
                                <table style='width: 80%; border-collapse: collapse;'align='center'>
                                    <tr>
                                        <th style='border: 1px solid gray; padding: 15px; font-size: 22px; text-align: center; background-color: lightblue; color: black;'>Date</th>
                                        <th style='border: 1px solid gray; padding: 15px; font-size: 22px; text-align: center; background-color: lightblue; color: black;'>Account Number</th>
                                        <th style='border: 1px solid gray; padding: 15px; font-size: 22px; text-align: center; background-color: lightblue; color: black;'>Credit</th>
                                        <th style='border: 1px solid gray; padding: 15px; font-size: 22px; text-align: center; background-color: lightblue; color: black;'>Debit</th>
                                        <th style='border: 1px solid gray; padding: 15px; font-size: 22px; text-align: center; background-color: lightblue; color: black;'>Balance</th>
                                    </tr>", month, year);
        foreach (var op in operationsHistoriesByMonth)
        {
            sb.AppendFormat(@"<tr>
                                    <td style='border: 1px solid gray; padding: 15px; font-size: 22px; text-align: center;'>{0}</td>
                                    <td style='border: 1px solid gray; padding: 15px; font-size: 22px; text-align: center;'>{1}</td>
                                    <td style='border: 1px solid gray; padding: 15px; font-size: 22px; text-align: center; color:green'>{2}</td>
                                    <td style='border: 1px solid gray; padding: 15px; font-size: 22px; text-align: center; color:red'>-{3}</td>
                                    <td style='border: 1px solid gray; padding: 15px; font-size: 22px; text-align: center;'>{4}</td>
                                  </tr>", op.OperationTime, op.AccountID, !op.Debit ? op.TransactionAmount : "", op.Debit ? op.TransactionAmount : "", op.Balance);
        }
        sb.Append(@"
                                </table>
                            </body>
                        </html>");
        return sb.ToString();
    }
}
