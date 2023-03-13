var MyPlugin = {

    CreateCsv: function(mydata,filename)
    {

         var csvData = Pointer_stringify(mydata);
         var csvFileName = Pointer_stringify(filename);
         var dataSplit = csvData.split(",");

         var columns = new Array(dataSplit.length);

         for(var i = 0; i < columns.length; i++){

            var rowSplit = dataSplit[i].split("|");

            var rowDataTemp = new Array(3);
            rowDataTemp[0] = rowSplit[0];
            rowDataTemp[1] = rowSplit[1];
            rowDataTemp[2] = rowSplit[2];

            columns[i] = rowDataTemp;
         }

       
        var csvContent = "data:text/csv;charset=utf-8,";
        columns.forEach(function(infoArray, index){

            dataString = infoArray.join(",");
            csvContent += index < columns.length ? dataString+ "\n" : dataString;

        });

        var encodedUri = encodeURI(csvContent);
        var link = document.createElement("a");
        link.setAttribute("href", encodedUri);
        link.setAttribute("download", csvFileName);
        document.body.appendChild(link);
        link.click();


    }
};

mergeInto(LibraryManager.library, MyPlugin); 