app.controller('CreateTimeTableCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Create Table";

    //$scope.ShowDate = false;
    $rootScope.showLoading = false;
    $scope.GetFlagdata = function () {
        $scope.IsInstitute = false;
        $scope.IsSection = false;
        if ($cookies.get("typePrefix") == 'INS') {
            $scope.IsInstitute = true;
            $scope.IsSection = false;
        }
        else if ($cookies.get("typePrefix") == 'SEC') {
            $scope.IsSection = true;
            $scope.IsInstitute = false;
        }
        else {
            $scope.IsInstitute = false;
            $scope.IsSection = false;
        }
    }
    $scope.cancelMstTimeTableMasterAdd = function () {
        $scope.filter = {
     
            ProgrammeId:0,
            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammePartTermId: 0,
            BranchId:0
        };
    };

    $scope.ExamSlotMasterListGetActiveList = [];
    $scope.defaultSlotList = [];
    var slotObj = {
        Id: 0,
        SlotName: "Select Slot"
    }
    $scope.defaultSlotList.push(slotObj);


    $scope.getExamSlotMasterListGetActive = function () {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/ExamSlotMaster/ExamSlotMasterListGetActive',
            data: {},
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {

                    $scope.tempList = response.obj;
                    $scope.ExamSlotMasterListGetActiveList = $scope.defaultSlotList.concat($scope.tempList);

                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    }


    $scope.cancelMstTimeTableMasterAdd();
    $scope.pendingTimeTableMasterGetPending = function () {

        $rootScope.showLoading = true;
        $scope.IsInstitute = false;
        $scope.IsSection = false;
        $scope.ShowConfirm = true;
        $http({
            method: 'POST',
            url: 'api/TimeTableMaster/GetTimeTableConfig',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {

                    $scope.TTC = response.obj;
                    $http({
                        method: 'POST',
                        url: 'api/TimeTableMaster/TimeTableMasterGetPending',
                        data: $scope.filter,
                        headers: { "Content-Type": 'application/json' }
                    })
                        .success(function (response) {
                            $rootScope.showLoading = false;

                            if (response.response_code != "200") {
                                alert(response.obj);
                            }
                            else {
                                $scope.TimeTableMasterGetPendingList = response.obj;

                                $scope.timetableTableParams = new NgTableParams({
                                    count: 1000
                                }, {
                                    dataset: $scope.TimeTableMasterGetPendingList
                                });
                                $scope.getExamSlotMasterListGetActive();
                                console.log($scope.timetableTableParams.data);

                                console.log($scope.TimeTableMasterGetPendingList);

                                for (var i in $scope.TimeTableMasterGetPendingList) {
                                    if ($scope.TimeTableMasterGetPendingList[i].ExamSlotId == null || $scope.TimeTableMasterGetPendingList[i].ExamDate == null || $scope.TimeTableMasterGetPendingList[i].Sequence == null) {
                                        $scope.ShowConfirm = true;
                                    }
                                }
                                if ($cookies.get("typePrefix") == 'INS') {
                                    $scope.IsInstitute = true;
                                    $scope.IsSection = false;
                                }
                                else if ($cookies.get("typePrefix") == 'SEC') {
                                    $scope.IsSection = true;
                                    $scope.IsInstitute = false;
                                }
                                else {
                                    $scope.IsInstitute = false;
                                    $scope.IsSection = false;
                                }
                            }
                            /*          $scope.TimeTableMasterGetPendingList*/
                        })
                        .error(function (res) {
                            $rootScope.showLoading = false;
                            alert(res.obj);
                        });
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    $scope.GetTTCConfirm = function () {
        $scope.filter.TTM = $scope.TimeTableMasterGetPendingList;
        $http({
            method: 'POST',
            url: 'api/TimeTableMaster/TimeTableMasterIsActive',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.pendingTimeTableMasterGetPending();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.GetTTCUnConfirm = function () {       
        $scope.filter.TTM = $scope.TimeTableMasterGetPendingList;
        
        $http({
            method: 'POST',
            url: 'api/TimeTableMaster/TimeTableMasterIsInactive',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.pendingTimeTableMasterGetPending();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    // for edit timetable with condition

    $scope.timetableFlag = false;
    $scope.editTimetable = function (data) {
   
        data.timetableFlag = true;
    }
     // for save timetable with condition
    $scope.saveTimetable = function (data) {
        $scope.timetableFlag = true;
       
        if (data.Id !== 0) {
       
            $scope.editTimeTableMasterEdit(data);

        } else if (data.Id === 0) {
      
            $scope.saveTimeTableMasterAddInBulk(data);
        }
     
    }

     // for edit timetable
    $scope.editTimeTableMasterEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.timetableFlag = true;
 /*       $scope.timetable = data;*/
        
        $http({
            method: 'POST',
       /*   url: 'api/TimeTableMaster/MstTimeTableMasterUpdate',*/
            url: 'api/TimeTableMaster/TimeTableMasterEdit',
            data: data ,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.timetableFlag = true;
                    $scope.pendingTimeTableMasterGetPending();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

     // for save timetable
    $scope.saveTimeTableMasterAddInBulk = function (data) {

        $rootScope.showLoading = true;
        $scope.timetableFlag = true;
   /*     $scope.timetable = data;*/
 
        $http({
            method: 'POST',
        /*    url: 'api/TimeTableMaster/MstTimeTableMasterAddInBulk',*/
            url: 'api/TimeTableMaster/TimeTableMasterAddInBulk',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.timetableFlag = false;
                    $scope.pendingTimeTableMasterGetPending();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for delete timetable
    $scope.deleteMstTimeTableClear = function (data) {

        $http({
            method: 'POST',
            url: 'api/TimeTableMaster/MstTimeTableClear',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.pendingTimeTableMasterGetPending();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    
    $scope.GetScheduleDate = function (data) {
        $scope.ScheduleDates = {};
        $scope.ShowDate = false;
        for (var i in $scope.FacultyExamMapListGetActiveList) {
            if ($scope.FacultyExamMapListGetActiveList[i].Id == data) {
                $scope.ScheduleDates.StartDateOfExam = $scope.FacultyExamMapListGetActiveList[i].StartDateOfExam;
                $scope.ScheduleDates.EndDateOfExam = $scope.FacultyExamMapListGetActiveList[i].EndDateOfExam;
                $scope.ShowDate = true;
            }
        }
    }

    $scope.GetTTC = function () {
        $http({
            method: 'POST',
            url: 'api/TimeTableMaster/GetTTCDetail',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    $scope.TTC = response.obj;

                    $scope.TTCTableParams = new NgTableParams({
                        count: 1000
                    }, {
                            dataset: $scope.TTC
                    });
                    //if ($cookies.get("typePrefix") == 'INS') {
                    //    $scope.IsInstitute = true;
                    //    $scope.IsSection = false;
                    //}
                    //else if ($cookies.get("typePrefix") == 'SEC') {
                    //    $scope.IsSection = true;
                    //    $scope.IsInstitute = false;
                    //}
                    //else {
                    //    $scope.IsInstitute = false;
                    //    $scope.IsSection = false;
                    //}

                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
    
    $scope.displayTT = function (DATA) {
        console.log(DATA);
        debugger;
        $http({
            method: 'POST',
            url: 'api/TimeTableMaster/GetTT',
            data: DATA,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.TTData = response.obj;
                    $scope.TTC = DATA;
                    $scope.TTC.TTData = $scope.TTData;
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.GetTTCPublish = function (DATA) {
        $http({
            method: 'POST',
            url: 'api/TimeTableMaster/TTCPublish',
            data: DATA,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.GetTTC();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.GetTTCUnPublish = function (DATA) {
        $http({
            method: 'POST',
            url: 'api/TimeTableMaster/TTCUnPublish',
            data: DATA,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.GetTTC();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.GetTTCDisplayOn = function (DATA) {
        $http({
            method: 'POST',
            url: 'api/TimeTableMaster/TTCDisplayOn',
            data: DATA,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.GetTTC();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.GetTTCDisplayOff = function (DATA) {
        $http({
            method: 'POST',
            url: 'api/TimeTableMaster/TTCDisplayOff',
            data: DATA,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.GetTTC();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };


    $scope.ExportDatatoExcel = function () {
        
        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "TimeTable" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'TimeTable - ' + $scope.TTC.ExamMaster + ' - ' + $scope.TTC.FacultyExamMap + ' - ' + $scope.TTC.PartTermName + ' - ' + $scope.TTC.Branch+' ' ,
            },
           
            columns: [
                { columnid: 'PaperName', title: 'Paper Name' },
                { columnid: 'TeachingLearningMethod', title: 'Teaching Learning Method' },
                { columnid: 'AssetsmentMethodName', title: 'Assetsment Method Name' },
                { columnid: 'AssetmentType', title: 'Assetment Type ' },
                { columnid: 'ExamDate', title: 'Exam Date' },
                { columnid: 'Sequence', title: 'Sequence' },
                { columnid: 'ExamSlotName', title: 'Exam Slot Name ' },
               
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.TTC.TTData]);
    };
});