app.controller('ReportForJrSupervisorIAExamCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $filter) {

    $rootScope.pageTitle = "Report For Jr. Supervisor IA-Exam";
    $scope.FlagList2 = false;
    $scope.FlagBlockwiseList = false;
  
    /*Reset All Dropdown*/
    $scope.resetJRSP = function () {
        $scope.JRSPIA = {}; 
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

    //Get for Institute
    $scope.getFacultyById = function () {
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            data: $scope.JRSPIA,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.JRSPIA = response.obj[0];
                //$scope.JrSuperVisorReportIAProgIncPartTermGet();
            })
            .error(function (res) {
                alert(res);
            });
    };


    //Get for ExamEventMaster List
    $scope.ExamEventMasterGet = function () {

        $http({
            method: 'POST',
            url: 'api/ReportForJrSupervisorIAExam/ExamEventMasterGet',
            data: $scope.JRSPIA,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamEventList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    ////Get for JrSuperVisorReportIAProgIncPartTermGet List
    //$scope.JrSuperVisorReportIAProgIncPartTermGet = function () {
    //    debugger
    //    var InstituteId = { InstituteId: $scope.JRSPIA.InstituteId };

    //    $http({
    //        method: 'POST',
    //        url: 'api/ReportForJrSupervisorIAExam/JrSuperVisorReportIAProgIncPartTermGet',
    //        data: InstituteId,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $scope.ProgIncPartTermList = response.obj;
    //        })
    //        .error(function (res) {
    //            alert(res);
    //        });
    //};

    //Get Paper list by event and ProgInstPartTerm

    $scope.JrSuperVisorReportIAPaperList = function () {
        
        $scope.checkDataExists = false;

        //if ($scope.JRSPIA.ExamEvent.Id == null || $scope.JRSPIA.ExamEvent.Id == "" || $scope.JRSPIA.ExamEvent.Id === undefined) {
        //    alert("Please Select Exam Event...!");
        //}
        //else if ($scope.JRSPIA.ProgInstancePartTerm.ProgInstPartTermId == null || $scope.JRSPIA.ProgInstancePartTerm.ProgInstPartTermId == "" || $scope.JRSPIA.ProgInstancePartTerm.ProgInstPartTermId === undefined) {
        //    alert("Please Select Programme Name...!");
        //}
        //else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/ReportForJrSupervisorIAExam/JrSuperVisorReportIAPaperList',
                data: $scope.examseat,
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

                        $scope.IAPaperListTableParams = new NgTableParams({}, { dataset: response.obj });
                        $scope.IAPaperListData = response.obj;
                        $scope.offSpinner();
                        if (response.obj.length == 0) {
                            $scope.checkDataExists = true;
                        }
                    }

                })
                .error(function (res) {
                    alert(res);
                });
        /*}*/
    }

    //Get Students List for IA Exams
    $scope.JrSuperVisorReportIAStudentsList = function (ExamDate, PaperId, PaperCode, PaperName) {

        $scope.FlagList2 = true;

        $scope.IADataModel = {};
        $scope.IADataModel.ExamEventId = $scope.examseat.ExamMasterId;
        $scope.IADataModel.BranchId = $scope.examseat.BranchId;
        $scope.IADataModel.ProgrammePartTermId = $scope.examseat.ProgrammePartTermId;
        $scope.IADataModel.ExamDate = ExamDate;
        $scope.IADataModel.PaperId = PaperId;
        $scope.IADataModel.PaperCode = PaperCode;
        $scope.IADataModel.PaperName = PaperName;
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/ReportForJrSupervisorIAExam/JrSuperVisorReportIAStudentsList',
            data: $scope.IADataModel,
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
                  /*  $scope.StudentCountBlockWise = $scope.ResLength;*/
                    $scope.IAStudentsListTableParams = new NgTableParams({
                        page: 1,
                        count: $scope.ResLength
                    },
                        { counts: [], dataset: response.obj });
                    $scope.IAStudentsListData = response.obj;

                    //For HTML View
                    $scope.IADataModel.ExamEventName = $scope.IAStudentsListData[0].ExamEventName;
                    $scope.IADataModel.ProgrammePartTermName = $scope.IAStudentsListData[0].ProgrammePartTermName;
                    $scope.IADataModel.FromSeatNo = $scope.IAStudentsListData[0].FromSeatNo;
                    $scope.IADataModel.ToSeatNo = $scope.IAStudentsListData[0].ToSeatNo;
                    $scope.IADataModel.TotalStudentCount = $scope.IAStudentsListData[0].TotalStudentCount;

                    $scope.offSpinner();
                    var Array1 = [];
                    $scope.SeatNoList = $scope.IAStudentsListData;
                    for (i = 0; i < $scope.SeatNoList.length; i++) {
                        Array1[i] = $scope.SeatNoList[i].SeatNumber;
                    }

                    $scope.res = $scope.spliceIntoChunks(Array1, 7);


                    
                    if (response.obj.length == 0) {
                        $scope.checkDataExists = true;
                    }
                }

            })
            .error(function (res) {
                alert(res);
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

        $state.go('ReportForJrSupervisorIAExam');
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
        var ExcelFileName = "Jr.SuperVisor_Report_for_IA" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: '<label style=font-size:15px;> Jr.SuperVisor Report on </label>' + DateAndTime + '<br>' +
                    'Programme Name : ' + $scope.IADataModel.ProgrammePartTermName + '<br>' +
                    'Paper Name : ' + $scope.IADataModel.PaperName + '<br>' +
                    'Paper Code : ' + $scope.IADataModel.PaperCode + '<br>' +
                    'Event : ' + $scope.IADataModel.ExamEventName + '<br>' +
                    'Exam Date : ' + $scope.IADataModel.ExamDate + '<br>',
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
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.IAStudentsListData]);
    };

});