app.controller('ReportForJrSupervisorCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $filter) {

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
            url: 'api/ReportForJrSupervisor/ExamVenueGet',
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
            url: 'api/ReportForJrSupervisor/ExamEventMasterGet',
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

    //Get for ExamBlocks List
    $scope.ExamBlocksGet = function (ProgInstPTId, InstancePartTermName, PaperName, PaperCode, PaperId, ExamDate, SlotName, StudentCount) {
        $scope.FlagList2 = true;
        $scope.FlagBlockwiseList = false;

        $scope.JRSPEB = {};
        $scope.JRSPEB.ExamEventId = $scope.JRSP.ExamEvent.Id;
        $scope.JRSPEB.ExamVenueId = $scope.JRSP.ExamVenue.Id;
        $scope.JRSPEB.PaperId = PaperId;
        $scope.JRSPEB.ProgrammeInstancePartTermId = ProgInstPTId;
        $scope.JRSPEB.InstancePartTermName = InstancePartTermName;
        $scope.JRSPEB.PaperName = PaperName;
        $scope.JRSPEB.PaperCode = PaperCode;
        $scope.JRSPEB.ExamDate = ExamDate;
        $scope.JRSPEB.SlotName = SlotName;
        $scope.JRSPEB.StudentCount = StudentCount;

        $http({
            method: 'POST',
            url: 'api/ReportForJrSupervisor/JrSuperVisorReportExamBlocksGet',
            data: $scope.JRSPEB,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamBlockNameList = response.obj;
                $scope.FlagBlockwiseList = false;
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
                url: 'api/ReportForJrSupervisor/JrSuperVisorReportList1',
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
    $scope.GetJrSupervisorReportList2 = function (JRSPEB,a) {
  
        $scope.Block = {};
        
        $scope.Block.ExamBlockName = a.ExamBlockName;
        $scope.Block.ExamBlockId = a.ExamBlockId;
        $scope.Block.VanueName = a.VanueName;
        $scope.Block.StudentCount = a.StudentCount;
        $scope.Block.FromSeatNumber = a.FromSeatNumber;
        $scope.Block.ToSeatNo = a.ToSeatNo;

        $scope.checkDataExists = false;
        $scope.JRSP.ExamEventId = $scope.JRSP.ExamEvent.Id;
        $scope.JRSP.ExamVenueId = $scope.JRSP.ExamVenue.Id;
        $scope.JRSP.ExamBlockId = $scope.JRSP.ExamBlock.ExamBlockId;
        $scope.Block_Name = $scope.JRSP.ExamBlock.ExamBlockName;

        $localStorage.JrReport = {};
        $localStorage.JrReport.ProgrammeInstancePartTermId = JRSPEB.ProgrammeInstancePartTermId;
        $localStorage.JrReport.InstancePartTermName = JRSPEB.InstancePartTermName;
        $localStorage.JrReport.PaperName = JRSPEB.PaperName;
        $localStorage.JrReport.PaperCode = JRSPEB.PaperCode;
        $localStorage.JrReport.PaperId = JRSPEB.PaperId;
        $localStorage.JrReport.ExamDate = JRSPEB.ExamDate;
        $localStorage.JrReport.SlotName = JRSPEB.SlotName;
        $localStorage.JrReport.StudentCount = JRSPEB.StudentCount;

        $scope.JRSP.ProgrammeInstancePartTermId = $localStorage.JrReport.ProgrammeInstancePartTermId;
        $scope.InstancePartTermName = $localStorage.JrReport.InstancePartTermName;
        $scope.JRSP.PaperId = $localStorage.JrReport.PaperId;

        $scope.PaperName = $localStorage.JrReport.PaperName;
        $scope.PaperCode = $localStorage.JrReport.PaperCode;
        $scope.ExamDate = $localStorage.JrReport.ExamDate;
        $scope.SlotName = $localStorage.JrReport.SlotName;
        $scope.StudentCount = $localStorage.JrReport.StudentCount;
        $scope.ExamEventName = $scope.JRSP.ExamEvent.DisplayName;
        $scope.ExamVenueName = $scope.JRSP.ExamVenue.DisplayName;

        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/ReportForJrSupervisor/JrSuperVisorReportList2',
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
                    $scope.ResLength = response.obj.length;
                    $scope.StudentCountBlockWise = $scope.ResLength;
                    $scope.VenueEventListTableParams = new NgTableParams({
                        page: 1,
                        count: $scope.ResLength
                    },
                    { counts: [], dataset: response.obj });
                    $scope.VenueEventListData = response.obj;
                    $scope.FlagBlockwiseList = true;
                    var Array1 = [];
                    $scope.SeatNoList = $scope.VenueEventListData;
                    for (i = 0; i < $scope.SeatNoList.length ; i++) {
                        Array1[i] = $scope.SeatNoList[i].SeatNumber;
                    }
                    
                  $scope.res =  $scope.spliceIntoChunks(Array1, 7);


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

        for (var i = 0; i < arr.length; i+= chunkSize) {
            res.push(arr.slice(i, i + chunkSize));
        }

        return res;
    };

    /*Get Back to main page*/
    $scope.GetBack = function () {

        $state.go('ReportForJrSupervisor');
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
                    'Event : ' + $scope.ExamEventName + '<br>' + 
                    'Block Name : ' + $scope.Block_Name + '   (Total Students : ' + $scope.StudentCountBlockWise + ')' +'<br>' ,
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
            url: 'api/ReportForJrSupervisor/PDFGeneratedForJrSP',
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