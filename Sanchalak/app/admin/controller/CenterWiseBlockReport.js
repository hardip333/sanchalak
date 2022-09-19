app.controller('CenterWiseBlockReportCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Center Wise Block Report";
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    /*CancelCenterWiseBlockReport*/
    $scope.cancelCenterWiseBlockReport = function () {
        $scope.CenterBlockReport = {};
        $scope.IsTableVisible = false;
        $scope.IsExcelButton = false;
    };

    $scope.ExamEventGet = function () {
        $scope.ExamEventList = {};
        $http({
            method: 'POST',
            url: 'api/CenterWiseBlockReport/ExamEventGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.ExamEventList = {};

                    }
                }
                else {
                    $scope.ExamEventList = response.obj;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.VenueGetByExamMasterId = function () {
  
        $scope.VenueList = {};
            $http({
                method: 'POST',
                url: 'api/CenterWiseBlockReport/VenueGetByExamMasterId',
                data: $scope.CenterBlockReport,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $scope.VenueList = {};
                    }
                    else {
                        $scope.VenueList = response.obj;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        
    };

    $scope.CenterGetByVenueId = function () {
        $scope.CenterList = {};
        $http({
            method: 'POST',
            url: 'api/CenterWiseBlockReport/ExamVenueExamCenterGetByVenueId',
            data: $scope.CenterBlockReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.CenterList = {};
                }
                else {
                    $scope.CenterList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.SlotGetByExamCenterExamVenueId = function () {
        $scope.SlotMasterList = {};
        $http({
            method: 'POST',
            url: 'api/CenterWiseBlockReport/SlotMasterGetByExamVenueExamCenterId',
            data: $scope.CenterBlockReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.SlotMasterList = {};
                }
                else {
                    $scope.SlotMasterList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.CenterWiseBlockReportById = function () {
        if ($scope.CenterBlockReport.ExamMasterId === null || $scope.CenterBlockReport.ExamMasterId === undefined ||
            $scope.CenterBlockReport.ExamVenueId === null || $scope.CenterBlockReport.ExamVenueId === undefined ||
            $scope.CenterBlockReport.ExamVenueExamCenterId === null || $scope.CenterBlockReport.ExamVenueExamCenterId === undefined ||
            $scope.CenterBlockReport.ExamSlotId === null || $scope.CenterBlockReport.ExamSlotId === undefined) {

            alert("please check all fields !!!");
        }
        else {
            $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/CenterWiseBlockReport/CenterWiseBlockReportGetById',
            data: $scope.CenterBlockReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.offSpinner();
                    $scope.NoRecLabel = true;
                    $scope.IsTableVisible = false;
                    $scope.IsExcelButton = false;
                   
                }
                else {
                    $scope.offSpinner();
                    $scope.NoRecLabel = false;
                    $scope.IsTableVisible = true;
                    $scope.IsExcelButton = true;
                    $scope.CenterWiseBlockReportTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.CenterWiseBlockReportList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
        }
    };

    $scope.exportData = function () {


        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "CenterWiseBlockReport" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:20px;font-weight:bold',
            caption: {
                title: 'CenterWiseBlockReport|Date and Time: ' + '<br>' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ExameventName', title: 'Exam Event Name' },
                { columnid: 'ExamVenueName', title: 'ExamVenueName' },
                { columnid: 'SlotName', title: 'Exam SlotName' },
                { columnid: 'CenterName', title: 'Exam Center Name' },
                { columnid: 'Date', title: 'Exam Date' },
                { columnid: 'BlockName', title: 'Exam Block Name' },
                { columnid: 'PaperCode', title: 'Exam Paper Code' },
                { columnid: 'PaperName', title: 'Exam Paper Name' },
          
                { columnid: 'StudentCount', title: 'Student Count' },
               






            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.CenterWiseBlockReportList]);
    };
    
    



});