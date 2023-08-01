

    var gridviewID = "GridView1";
    var gridview = null;
    var gridviewFreeze = null;
    var selectedRowIndex = -1;

    $(document).ready(function () {
        gridview = $('#' + gridviewID);
        gridviewFreeze = $('#' + gridviewID + 'Freeze');
    });

    function RowMouseOver(rowIndex) {
        if (selectedRowIndex == rowIndex) return;
        gridview[0].rows[rowIndex + 1].className = 'GridviewScrollItemHover';

        if (gridviewFreeze[0])
            gridviewFreeze[0].rows[rowIndex + 1].className = 'GridviewScrollItemHover';
    }

    function RowMouseOut(rowIndex) {
        if (selectedRowIndex == rowIndex) return;
        gridview[0].rows[rowIndex + 1].className = 'GridviewScrollItem';

        if (gridviewFreeze[0])
            gridviewFreeze[0].rows[rowIndex + 1].className = 'GridviewScrollItem';
    }

    function RowSelect(rowIndex) {
        if (selectedRowIndex == rowIndex) return;
        RowReset(selectedRowIndex);
        selectedRowIndex = rowIndex;
        gridview[0].rows[rowIndex + 1].className = 'GridviewScrollItemSelected';

        if (gridviewFreeze[0])
            gridviewFreeze[0].rows[rowIndex + 1].className = 'GridviewScrollItemSelected';
    }

    function RowReset(rowIndex) {
        gridview[0].rows[rowIndex + 1].className = 'GridviewScrollItem';
        if (gridviewFreeze[0])
            gridviewFreeze[0].rows[rowIndex + 1].className = 'GridviewScrollItem';
    } 
