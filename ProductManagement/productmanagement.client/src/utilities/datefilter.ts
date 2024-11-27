
// comparer for use with ag-grids date column while removing 'time' part of the date
export const dateComparator = (filterLocalDateAtMidnight: any, cellValue: string) => { 
    const cellDate = new Date(cellValue.split('T')[0]);
    const filterDate = new Date(filterLocalDateAtMidnight);

    if (filterDate.getTime() === cellDate.getTime()) {
        return 0;
    }
    if (cellDate < filterDate) {
        return -1;
    }
    if (cellDate > filterDate) {
        return 1;
    }
    return 0;
};

// user friendly input for date filter
export const dateFilterParams = {
    comparator: dateComparator,
    inRangeFloatingFilterDateFormat: 'dd-MM-yyyy'
}