using FinanceMyLife.Data.Enums;
using FinanceMyLife.Data.Interfaces;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace FinanceMyLife.Data.Services;

public class CustomDataAdaptor<T> : DataAdaptor where T : class
{
    private readonly IDataAccessLayer<T> _dataAccessLayer;

    public CustomDataAdaptor(IDataAccessLayer<T> dataAccessLayer)
    {
        _dataAccessLayer = dataAccessLayer;
    }

    public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
    {
        var query = dataManagerRequest.Params;

        IEnumerable<T> result = await _dataAccessLayer.GetAsync(dataManagerRequest);


        object controllerTypeVal = string.Empty;
        bool hasControllerTypeKey = dataManagerRequest.Params?.TryGetValue(ParamsEnum.ControllerType.ToString(), out controllerTypeVal) ?? false;

        if (hasControllerTypeKey && controllerTypeVal?.ToString() == ControllerTypeEnum.dropdown.ToString())
        {
            return result;
        }
        else
        {
            int count = await _dataAccessLayer.GetCountAsync();
            return dataManagerRequest.RequiresCounts ? new DataResult() { Result = result, Count = count } : count;
        }

    }

    public override async Task<object> InsertAsync(DataManager dataManager, object data, string key)
    {
        try
        {
            await _dataAccessLayer.AddAsync(data as T);
            return data;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public override async Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key)
    {
        await _dataAccessLayer.UpdateAsync(data as T, keyField);
        return data;
    }

    public override async Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key)
    {
        await _dataAccessLayer.RemoveAsync(Convert.ToInt32(primaryKeyValue));
        return primaryKeyValue;
    }

    public override async Task<object> BatchUpdateAsync(DataManager dataManager, object changedRecords, object addedRecords, object deletedRecords, string keyField, string key, int? dropIndex)
    {
        return Task.FromResult<object>(null);
    }

}