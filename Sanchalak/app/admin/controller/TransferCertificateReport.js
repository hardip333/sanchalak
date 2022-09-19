app.controller('TransferCertificateReportCtrl', function ($scope, $http, $filter, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "TransferCertificateReport";
    $scope.resetTransferCertificateReport = function () {
        $scope.TransferCertificateReport = {};
    }
    $scope.ShowTC = false;
    
    

    $scope.getFacultyById = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/TransferCertificateReport/FacultyGetById',
            data: { Id: $cookies.get('InstituteId') },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.FacultyList = response.obj;

                // $scope.Faculty = response.obj; // Krunal's code               



            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.ProgrammeGetbyFacultyId = function () {
        var FacId = { FacultyId: $scope.TransferCertificateReport.FacultyId }
        $http({
            method: 'Post',
            url: 'api/TransferCertificateReport/ProgrammeGetByFacId',
            data: FacId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.ProgrammeList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.GetCerti = function () {
        
        $http({
            method: 'Post',
            url: 'api/TransferCertificateReport/CertificateConfigurationGet',
            data: $scope.TransferCertificateReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.CertiList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getTransferCertificateReport = function () {
       // debugger
        var ObjTC = {
            
            FacultyId: $scope.TransferCertificateReport.FacultyId,
            ProgrammeId: $scope.TransferCertificateReport.ProgrammeId,
            CertiId: $scope.TransferCertificateReport.CertiId
        };
        //console.log(Obj)
        $http({
            method: 'POST',
            url: 'api/TransferCertificateReport/TransferCertificateReportGet',
            data: ObjTC,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //console.log(response.obj);
                    if (response.obj === "No Record Found") {

                        $scope.NoRecordFound = true;
                        $scope.TransferCertificateReportTableParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {
                        $scope.TransferCertificateReportTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        $scope.TC = response.obj;
                        $scope.TCData = response.obj[0];
                        $scope.ExcelTCReport = response.obj;
                        $scope.ShowTC = true;
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.exportTC = function () {

        if ($scope.ExcelTCReport == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        //alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "TransferCertificateReport_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:20px;font-weight:bold;',
            caption: {
                title: 'Transfer Certificate Report | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'NameAsPerMarksheet', title: 'Full Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'TCCode', title: 'Transfer Certification Code' },
                
                
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.ExcelTCReport]);
    };





})