app.controller('ApplicationPaymentReportCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Application Payment Report Ctrl";

    $scope.ApplicationPaymentReport = {}

    

    //$scope.test = "All";

    //$scope.InstPartList = []

    /*Reset Academic Year Level*/
    $scope.resetApplicationPaymentReport = function () {
        $scope.ApplicationPaymentReport = {};
    };

    /*Get Application Payment Report*/
    //$scope.getApplicationPaymentReport = function () {

    //    var data = new Object();

    //    $http({
    //        method: 'POST',
    //        url: 'api/ApplicationPaymentReport/ApplicationPaymentReportGet',
    //        data: data,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {

    //            $rootScope.showLoading = false;

    //            if (response.response_code == "0") {
    //                $state.go('login');

    //            } else if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //            }
    //            else {

    //                //$scope.getApplicationPaymentReport = response.obj;
    //                //$scope.refreshtable();
    //                $scope.ApplicationPaymentReportTableParams = new NgTableParams({
    //                }, {
    //                        dataset: response.obj
                        

    //                });

    //            }
                
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};

    /*Add Academic Year*/
    

    $scope.expand_row = function (Id) {
        let element = document.getElementById('expand' + Id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col" + Id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col" + Id).innerHTML = "+"
            element.add("collapse")
        }
    }

    //$scope.test1 = "All";

    $scope.getfilterpaymentdata = function (InstPartTermId) {


        //alert("Fromdate===" + $scope.FromDate);
        //alert("ToDate=====" + $scope.ToDate);

        $http({
            method: 'POST',
            url: 'api/ApplicationPaymentReport/ApplicationPaymentReportGet',
            data: {
                TransactionStatus: $scope.TransactionStatus,
                FromDate: $scope.FromDate,
                ToDate: $scope.ToDate
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.ApplicantByProgPartTermList = response.obj;
                //$scope.ApplicationPaymentReportTableParams = new NgTableParams({

                //}, { dataset: response.obj });
                $scope.ApplicationPaymentReportTableParams = new NgTableParams({

                }, {
                    dataset: response.obj


                });
                // debugger;
                $scope.exporttoexcelList = response.obj;
                //console.log($scope.exporttoexcelList);
                //$scope.test1 = $scope.test;

            })

            .error(function (res) {
                alert(res);
            });

    };

    /*Display Academic Year Data*/
    $scope.viewPaymentDetails = function (data) {        
        //$scope.PaymentDetails = data;
        console.log(data);
        $state.go('PaymentDetails', { obj: data });
    };


    $scope.exportApplicationPaymentReportData = function () {

        if ($scope.ApplicationPaymentReport == undefined) {



            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        var FacultyName = $localStorage.facultyName;
        //var ProgInstPartTermName = $scope.exporttoexcelList[0].InstancePartTermName;
        //var FacultyName = $scope.Institute.FacultyName;
        //var InstituteName = $scope.Institute.InstituteName;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();



        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ApplicationPaymentReport_" + DateWithoutDashed + time;



        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'FacultyName: ' + FacultyName + '<br>' +
                    '  Application Payment Reports | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No.' },
                { columnid: 'Id', title: 'Application Form No.' },
                { columnid: 'FirstName', title: 'FirstName' },
                { columnid: 'MiddleName', title: 'Middle Name' },
                { columnid: 'LastName', title: 'Last Name' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'TransactionStatus', title: 'Payment Status' },
                { columnid: 'TransactionDates', title: 'Transaction Date' },

            ],
        };



        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.exporttoexcelList]);



        // var FacultyName = $scope.Institute.FacultyName;
        // var InstituteName = $scope.Institute.InstituteName;
        // var LongDate = new Date($.now());
        // //alert(LongDate);



        // var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        // //alert(ShortDate);



        // var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        // //alert(DateWithoutDashed);



        // var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();                
        //// alert(time);

        // var dateAndTime = ShortDate + time;
        // var ExcelFileName = "ApplicationStatistics_" + ShortDate + time;     
        // //var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        // var uri = 'data:application/vnd.ms-excel;base64,'           
        //     , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2>Faculty Name: {FacultyName}, Institute Name: {InstituteName}</h2><h2>Application Statistics ({dateAndTime}) </h2>{table}</table></body></html>'
        //     , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        //     , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }





        // var table = document.getElementById("ApplicationStatsId");                
        // var filters = $('.ng-table-filters').remove();
        // var ctx = { worksheet: name || 'Worksheet', dateAndTime: dateAndTime || dateAndTime, FacultyName: FacultyName || FacultyName, InstituteName: InstituteName || InstituteName, table: table.innerHTML};       

        // $('.ng-table-sort-header').after(filters);
        // var url = uri + base64(format(template, ctx));

        // var a = document.createElement('a');
        // a.href = url;
        // a.download = ExcelFileName + '.xls';
        // a.click();
    };
    
	
//-------------Mohini's Code Start-------------//

    
    //Function for Get Faculty by Id
    $scope.getFacultyById = function () {
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            //data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //$scope.Faculty = response.obj[0];
                $scope.Institute = response.obj[0];
                //$scope.PostProgInst.FacultyId = $scope.Faculty.Id;
                $scope.getApplicationPaymentReportByInstitute();
               
            })
            .error(function (res) {
                alert(res);
            });
    };


    /*Get Application Payment Report*/
    $scope.getApplicationPaymentReportByInstitute = function () {

        var InstituteId = {
            InstituteId: $scope.Institute.InstituteId
        };
        //var data = new Object();

        $http({
            method: 'POST',
            url: 'api/ApplicationPaymentReport/ApplicationPaymentReportGetByInstitute',
            data: InstituteId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //console.log(response.obj);
                    //$scope.getApplicationPaymentReport = response.obj;
                    //$scope.refreshtable();
                    $scope.ApplicationPaymentReportTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj


                    });

                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    //Function for Export data to Excel for Eligible Applicant List
    $scope.ExportDatatoExcel = function () {

        var FacultyName = $scope.Institute.FacultyName;
        var InstituteName = $scope.Institute.InstituteName;
        var LongDate = new Date($.now());
        //alert(LongDate);
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        //alert(ShortDate);
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        //alert(DateWithoutDashed);
        var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();
        // alert(time);
        var dateAndTime = ShortDate + time;
        var ExcelFileName = "APPLICATION_PAYMENT_REPORT" + ShortDate + time;
        //var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;
        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2>Faculty Name: {FacultyName}, Institute Name: {InstituteName}</h2><h2>APPLICATION PAYMENT REPORT ({dateAndTime}) </h2>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

        var table = document.getElementById("paymentstatus");
        var filters = $('.ng-table-filters').remove();
        var ctx = { worksheet: name || 'Worksheet', dateAndTime: dateAndTime || dateAndTime, FacultyName: FacultyName || FacultyName, InstituteName: InstituteName || InstituteName, table: table.innerHTML };

        $('.ng-table-sort-header').after(filters);
        var url = uri + base64(format(template, ctx));

        var a = document.createElement('a');
        a.href = url;
        a.download = ExcelFileName + '.xls';
        a.click();
    };

//-------------Mohini's Code End-------------//


    
    

});

        