app.controller('InwardStatCtrl', function ($scope, $http, $rootScope, $localStorage, $filter, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Inward Statistics Detail";
   
    $scope.InwdStat = {};
    $scope.InwardStatListFlag = false;
    $scope.InwardStatListByStuCountFlag = false;

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.resetInwdStat = function () {
        $scope.InwdStat = {};
    };

    //Get for Exam Event List
    $scope.getExamEventMasterList = function () {
     
        $http({
            method: 'POST',
            url: 'api/InwardStatistics/ExamEventMasterGet',
            data: $scope.InwdStat,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamEventList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get for Faculty List
    $scope.getFacultyList = function () {
        $http({
            method: 'POST',
            url: 'api/InwardStatistics/MstFacultyGet',
            data: $scope.InwdStat,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacultyList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get for MstProgramme List
    $scope.getMstProgrammeList = function () {

        $http({
            method: 'POST',
            url: 'api/InwardStatistics/MstProgrammeGet',
            data: $scope.InwdStat,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammeList = response;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get for Specialisation List
    $scope.getSpecialisationList = function () {

        $http({
            method: 'POST',
            url: 'api/InwardStatistics/SpecialisationGet',
            data: $scope.InwdStat,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.SpecialisationList = response;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get for Programme Part List
    $scope.getProgrammePartList = function () {

        $http({
            method: 'POST',
            url: 'api/InwardStatistics/ProgrammePartListGetbyFaculty',
            data: $scope.InwdStat,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammePartList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get for Programme Part Term List
    $scope.getProgrammePartTermList = function () {
        
        $http({
            method: 'POST',
            url: 'api/InwardStatistics/ProgrammePartTermListGetbyFaculty',
            data: $scope.InwdStat,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammePartTermList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get for Programme Details
    $scope.getInwardStatProgrammeDetails = function () {

        $http({
            method: 'POST',
            url: 'api/InwardStatistics/InwardStatProgrammeDetailsGet',
            data: $scope.InwdStat,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammeDetailsList = response.obj;
                $scope.EventName = $scope.ProgrammeDetailsList[0].EventName;
                $scope.FacultyName = $scope.ProgrammeDetailsList[0].FacultyName;
                $scope.ProgrammeName = $scope.ProgrammeDetailsList[0].ProgrammeName;
                $scope.BranchName = $scope.ProgrammeDetailsList[0].BranchName;
                $scope.PartName = $scope.ProgrammeDetailsList[0].PartName;
                $scope.PartTermName = $scope.ProgrammeDetailsList[0].PartTermName;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get for InwardStat List
    $scope.getInwardStatList = function (ExamMasterId, SpecialisationId, ProgrammePartTermId) {

        $scope.ExamMasterId = ExamMasterId;
        $scope.SpecialisationId = SpecialisationId;
        $scope.ProgrammePartTermId = ProgrammePartTermId;

        $scope.InwardStatListFlag = false;
        $scope.InwardStatListByStuCountFlag = false;
        $scope.DataExistsInwardStat = false;

        if ($scope.InwdStat.ExamMasterId == null || $scope.InwdStat.ExamMasterId == undefined) {
            alert("Please Select Exam Event.");
        }
        else if ($scope.InwdStat.FacultyId == null || $scope.InwdStat.FacultyId == undefined) {
            alert("Please Select Faculty.");
        }
        else if ($scope.InwdStat.ProgrammeId == null || $scope.InwdStat.ProgrammeId == undefined) {
            alert("Please Select Programme.");
        }
        else if ($scope.InwdStat.SpecialisationId == null || $scope.InwdStat.SpecialisationId == undefined) {
            alert("Please Select Branch.");
        }
        else if ($scope.InwdStat.ProgrammePartId == null || $scope.InwdStat.ProgrammePartId == undefined) {
            alert("Please Select Programme Part.");
        }
        else if ($scope.InwdStat.ProgrammePartTermId == null || $scope.InwdStat.ProgrammePartTermId == undefined) {
            alert("Please Select Programme Part Term.");
        }
        else {

            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/InwardStatistics/InwardStatListGet',
                data: {
                    ExamMasterId: $scope.ExamMasterId,
                    SpecialisationId: $scope.SpecialisationId,
                    ProgrammePartTermId: $scope.ProgrammePartTermId
                },
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');
                        $scope.InwardStatListFlag = false;
                        $scope.offSpinner();

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $scope.InwardStatListFlag = false;
                        $scope.offSpinner();
                    }
                    else {
                        $scope.IWDataTableParams = new NgTableParams({

                            page: 1,
                            Count: 100
                        }, { dataset: response.obj });
                        $scope.InwardStatList = response.obj;
                        $scope.getInwardStatProgrammeDetails();
                        $scope.offSpinner();
                        $scope.InwardStatListFlag = true;
                        if (response.obj.length == 0) {
                            $scope.DataExistsInwardStat = true;
                        }
                    }
                })
                .error(function (res) {
                    alert(res);
                });
        }
    };

    //Get for InwardStat List By Student Count
    $scope.getInwardStatListByStuCount = function (ExamMasterId, SpecialisationId, ProgrammePartTermId, MstPaperId, PaperCode, PaperName, ExamDate, SlotName, StudentCount) {

        $scope.ExamMasterId = ExamMasterId;
        $scope.SpecialisationId = SpecialisationId;
        $scope.ProgrammePartTermId = ProgrammePartTermId;
        $scope.MstPaperId = MstPaperId;

        $scope.PaperCode = PaperCode;
        $scope.PaperName = PaperName;
        $scope.ExamDate = ExamDate;
        $scope.SlotName = SlotName;
        $scope.StudentCount = StudentCount;

        $scope.InwardStatListFlag = false;
        $scope.InwardStatListByStuCountFlag = false;
        $scope.DataExistsInwardStatStudent = false;

        $scope.onSpinner();

        $http({
            method: 'POST',
            url: 'api/InwardStatistics/InwardStatListGetByStuCount',
            data: {
                ExamMasterId: $scope.ExamMasterId,
                SpecialisationId: $scope.SpecialisationId,
                ProgrammePartTermId: $scope.ProgrammePartTermId,
                MstPaperId: $scope.MstPaperId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                    $scope.InwardStatListByStuCountFlag = false;
                    $scope.offSpinner();

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.InwardStatListByStuCountFlag = false;
                    $scope.offSpinner();
                }
                else {
                    $scope.IWStuDataTableParams = new NgTableParams({

                        page: 1,
                        Count: 100
                    }, { dataset: response.obj });
                    $scope.InwardStatListByStuCount = response.obj;
                    $scope.offSpinner();
                    $scope.InwardStatListByStuCountFlag = true;
                    if (response.obj.length == 0) {
                        $scope.DataExistsInwardStatStudent = true;
                    }
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Function for get back page
    $scope.backToInwardStatList = function () {
        $scope.InwardStatListFlag = true;
        $scope.InwardStatListByStuCountFlag = false;
    };

    //Export To Excel Code for Download Inward Statistics Report
    $scope.ExportToExcelInwardStatisticsList = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Inward_Statistics" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: '<label style=font-size:15px;> Inward Statistics Report on </label>' + DateAndTime + '<br>' +
               
                    'Exam Event : ' + $scope.EventName + '<br>' +
                    '' + $scope.FacultyName + '<br>' +
                    ''  + $scope.PartName + '-' + $scope.BranchName + '-' + $scope.PartTermName + '<br>' +
                    'Paper Name : ' + $scope.PaperName + '-' + '[' + $scope.PaperCode + ']' +'<br>' +
                    'Exam Date : ' + $scope.ExamDate + '<br>' +
                    'Slot Name : ' + $scope.SlotName + '<br>' +
                    'Total Student : ' + $scope.StudentCount + '<br>',
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr No.' },
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'StudentName', title: 'Student Name' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'EmailId', title: 'Email-Id' },
                { columnid: 'AppearanceType', title: 'Appearance Type' },
                { columnid: 'InwardStatus', title: 'Inward Status' },
                { columnid: 'InwardByTimestamp', title: 'Inward By Timestamp' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.InwardStatListByStuCount]);
    };

});



