app.controller('ReportForJrSupervisorAllBlockCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $filter) {

    $rootScope.pageTitle = "Report For Jr. Supervisor";
    $scope.FlagList2 = false;
    $scope.FlagBlockwiseList = false;

    /*Reset All Dropdown*/
    $scope.resetJRSP = function () {
        $scope.JRSP = {};
    };
    $scope.SeatNoList = {};
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.showFile = function (fileString) {
        console.log(fileString)
        //var mywindow = window.open('new div', 'height=800,width=800');
        var mywindow = window.open('new div', 'height=800,width=800');
        mywindow.document.write('<html><head><title>Show File</title>');
        mywindow.document.write('</head><body >');
        mywindow.document.write(`<img src="data:Image/png;base64,${fileString}"/>`);
        //myWindow.document.write('<iframe id=iFrame src="${fileString}" ></iframe>');

        mywindow.document.write('</body></html>');
        return true;
    };

    //Get for ExamVenue List
    $scope.ExamVenueGet = function () {

        $http({
            method: 'POST',
            url: 'api/ReportForJrSupervisorAllBlock/ExamVenueGet',
            data: $scope.JRSP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamVenueList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get for ExamEventMaster List
    $scope.ExamEventMasterGet = function () {

        $http({
            method: 'POST',
            url: 'api/ReportForJrSupervisorAllBlock/ExamEventMasterGet',
            data: $scope.JRSP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamEventList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get for JrSuperVisorReportExamCenter List
    $scope.JrSuperVisorReportExamCenterGet = function (ProgInstPTId, InstancePartTermName, PaperName, PaperCode, PaperId, ExamDate, SlotName, StudentCount, ExamBlockId, Block_Name) {
        $scope.FlagList2 = true;
        $scope.FlagBlockwiseList = false;
        
        $scope.JRSP.ExamVenueId = $scope.JRSP.ExamVenue.Id;
        $scope.JrReport = {};
        $scope.JrReport.ProgrammeInstancePartTermId = ProgInstPTId;
        $scope.JrReport.InstancePartTermName = InstancePartTermName;
        $scope.JrReport.PaperName = PaperName;
        $scope.JrReport.PaperCode = PaperCode;
        $scope.JrReport.PaperId = PaperId;
        $scope.JrReport.ExamDate = ExamDate;
        $scope.JrReport.SlotName = SlotName;
        $scope.JrReport.StudentCount = StudentCount;
        $scope.JrReport.ExamBlockId = ExamBlockId;
        $scope.JrReport.Block_Name = Block_Name;

        $http({
            method: 'POST',
            url: 'api/ReportForJrSupervisorAllBlock/JrSuperVisorReportExamCenterGet',
            data: $scope.JRSP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamCenterList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };


    /*Get ReportForJrSupervisor List1*/
    $scope.GetJrSupervisorReportList1 = function () {

        $scope.JRSP.ExamVenueId = $scope.JRSP.ExamVenue.Id;
        $scope.JRSP.ExamEventId = $scope.JRSP.ExamEvent.Id;

        $scope.checkDataExists = false;
        if ($scope.JRSP.ExamVenue.Id == null || $scope.JRSP.ExamVenue.Id == "" || $scope.JRSP.ExamVenue.Id === undefined) {
            alert("Please Select Exam Venue...!");
        }
        else if ($scope.JRSP.ExamEvent.Id == null || $scope.JRSP.ExamEvent.Id == "" || $scope.JRSP.ExamEvent.Id === undefined) {
            alert("Please Select Exam Event...!");
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/ReportForJrSupervisorAllBlock/JrSuperVisorReportList1',
                data: $scope.JRSP,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');
                        $scope.offSpinner();

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $scope.offSpinner();
                    }
                    else {

                        $scope.VenueEventTableParams = new NgTableParams({}, { dataset: response.obj });
                        $scope.VenueEventData = response.obj;
                        $scope.offSpinner();
                        if (response.obj.length == 0) {
                            $scope.checkDataExists = true;
                        }
                    }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Get ReportForJrSupervisor List2*/
    $scope.GetJrSupervisorReportList2 = function (JrReport) {
        
        $scope.FlagList2 = true;
        $scope.checkDataExists = false;
        $scope.JRSP.ExamEventId = $scope.JRSP.ExamEvent.Id;
        $scope.JRSP.ExamVenueExamCenterId = $scope.JRSP.ExamCenter.ExamVenueExamCenterId;
        $scope.CenterName = $scope.JRSP.ExamCenter.CenterName;

        $localStorage.JrReport.ProgrammeInstancePartTermId = JrReport.ProgInstPTId;
        $localStorage.JrReport.InstancePartTermName = JrReport.InstancePartTermName;
        $localStorage.JrReport.PaperName = JrReport.PaperName;
        $localStorage.JrReport.PaperCode = JrReport.PaperCode;
        $localStorage.JrReport.PaperId = JrReport.PaperId;
        $localStorage.JrReport.ExamDate = JrReport.ExamDate;
        $localStorage.JrReport.SlotName = JrReport.SlotName;
        $localStorage.JrReport.StudentCount = JrReport.StudentCount;
        $localStorage.JrReport.ExamBlockId = JrReport.ExamBlockId;
        $localStorage.JrReport.Block_Name = JrReport.Block_Name;

        $scope.JRSP.ProgrammeInstancePartTermId = $localStorage.JrReport.ProgrammeInstancePartTermId;
        $scope.InstancePartTermName = $localStorage.JrReport.InstancePartTermName;
        $scope.JRSP.PaperId = $localStorage.JrReport.PaperId;
        $scope.JRSP.ExamBlockId = $localStorage.JrReport.ExamBlockId;

        $scope.PaperName = $localStorage.JrReport.PaperName;
        $scope.PaperCode = $localStorage.JrReport.PaperCode;
        $scope.ExamDate = $localStorage.JrReport.ExamDate;
        $scope.SlotName = $localStorage.JrReport.SlotName;
        $scope.StudentCount = $localStorage.JrReport.StudentCount;
        $scope.Block_Name = $localStorage.JrReport.Block_Name;
        $scope.ExamEventName = $scope.JRSP.ExamEvent.DisplayName;
        $scope.ExamVenueName = $scope.JRSP.ExamVenue.DisplayName;

        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/ReportForJrSupervisorAllBlock/JrSuperVisorReportList2',
            data: $scope.JRSP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                    $scope.offSpinner();

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.offSpinner();
                }
                else {

                    $scope.BlockList = response.obj;


                    //$scope.VenueEventListTableParams = new NgTableParams({
                    //    page: 1,
                    //},
                    //    { counts: [], dataset: response.obj });
                    //$scope.VenueEventListData = response.obj;
                    $scope.FlagBlockwiseList = true;
                    for (var i in $scope.BlockList) {
                        var Array1 = [];
                        var SeatNoList = $scope.BlockList[i].StudentList;
                        for (j = 0; j < SeatNoList.length; j++) {
                            Array1[j] = SeatNoList[j].SeatNumber;
                        }
                        $scope.BlockList[i].res = $scope.spliceIntoChunks(Array1, 7);
                    }



                    //var Array1 = [];
                    //$scope.SeatNoList = $scope.VenueEventListData;
                    //for (i = 0; i < $scope.SeatNoList.length; i++) {
                    //    Array1[i] = $scope.SeatNoList[i].SeatNumber;
                    //}

                    //$scope.res = $scope.spliceIntoChunks(Array1, 7);


                    $scope.offSpinner();
                    if (response.obj.length == 0) {
                        $scope.checkDataExists = true;
                    }
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.spliceIntoChunks = function (arr, chunkSize) {
        var res = [];

        for (var i = 0; i < arr.length; i += chunkSize) {
            res.push(arr.slice(i, i + chunkSize));
        }

        return res;
    };

    /*Get Back to main page*/
    $scope.GetBack = function () {

        $state.go('ReportForJrSupervisorAllBlock');
        $scope.FlagList2 = false;
        $scope.FlagBlockwiseList = false;
    };

    //Export To Excel Code for Download Jr.SuperVisor Report
    $scope.ExportToExcelJrSPReport = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Jr.SuperVisor_Report" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: '<label style=font-size:15px;> Jr.SuperVisor Report on </label>' + DateAndTime + '<br>' +
                    'Programme Name : ' + $scope.InstancePartTermName + '<br>' +
                    'Paper Name : ' + $scope.PaperName + '<br>' +
                    'Paper Code : ' + $scope.PaperCode + '<br>' +
                    'Venue : ' + $scope.ExamVenueName + '<br>' +
                    'Event : ' + $scope.ExamEventName + '<br>',
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr No.' },
                { columnid: 'FullName', title: 'Full Name' },
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'SeatNumber', title: 'Seat Number' },
                { columnid: 'StudentSignature', title: 'Student Signature' },
                { columnid: 'StudentPhoto', title: 'Student Photo' },


            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.VenueEventListData]);
    };

    //Get for ExamVenue List
    $scope.PDFGeneratedForJrSP = function () {

        $http({
            method: 'POST',
            url: 'api/ReportForJrSupervisorAllBlock/PDFGeneratedForJrSP',
            data: $scope.JRSP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.PDFGeneratedForJrSPList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

});