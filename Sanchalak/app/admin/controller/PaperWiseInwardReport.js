app.controller('PaperWiseInwardReportCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Paper Wise Inward Table";

    $rootScope.showLoading = false;
    $scope.PaperWiseInward = {};

    $scope.cancelMstTimeTableMasterAdd = function () {
        $scope.filter = {

            ProgrammeId: 0,
            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammePartTermId: 0,
            BranchId: 0
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
    

    $scope.timetableFlag = false;
    $scope.editTimetable = function (data) {

        data.timetableFlag = true;
    }

    $scope.cancelPaperWiseInwardReport = function () {
        $scope.filter = {};
        $scope.IsTableVisible = false;
        $scope.IsExcelButton = false;
        $scope.NoRecLabel = false;
        $scope.showFormFlag = false;
    };


    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.getPaperWiseInwardReport = function () {
       
        if ($scope.filter.ExamMasterId == null || $scope.filter.ExamMasterId == undefined || $scope.filter.ExamMasterId == ""

        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Exam Event DropDownValue")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.filter.FacultyExamMapId == null || $scope.filter.FacultyExamMapId == undefined || $scope.filter.FacultyExamMapId == "")
        {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Schedule DropDownValue")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }

        else if ($scope.filter.ProgrammeId == null || $scope.filter.ProgrammeId == undefined || $scope.filter.ProgrammeId == "") {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Programme DropDownValue")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }

        else if ($scope.filter.BranchId == null || $scope.filter.BranchId == undefined || $scope.filter.BranchId == "") {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Branch DropDownValue")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }

        else if ($scope.filter.ProgrammePartTermId == null || $scope.filter.ProgrammePartTermId == undefined || $scope.filter.ProgrammePartTermId == "") {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select ProgrammePartTerm DropDownValue")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }


        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/PaperWiseInward/PaperWiseInwardReportGetByExamMasterId',
                data: $scope.filter,
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
                        $scope.showFormFlag = false;

                    }

                    else {

                        $scope.offSpinner();
                        $scope.PaperWiseInwardReport = response.obj;
                       
                        for (let i = 0; i < $scope.PaperWiseInwardReport.length; i++) {
                            $scope.PaperWiseInward.FacultyName = $scope.PaperWiseInwardReport[i].FacultyName;                        
                            $scope.PaperWiseInward.BranchName = $scope.PaperWiseInwardReport[i].BranchName;
                            $scope.PaperWiseInward.ProgrammeInstanceName = $scope.PaperWiseInwardReport[i].ProgrammeName;                        
                            $scope.PaperWiseInward.ProgrammeCode = $scope.PaperWiseInwardReport[i].ProgrammeCode;                        
                            $scope.PaperWiseInward.ProgrammeModeName = $scope.PaperWiseInwardReport[i].ProgrammeModeName;                        
                            $scope.PaperWiseInward.InstancePartTermName = $scope.PaperWiseInwardReport[i].PartTermName;

                        }
                        $scope.showFormFlag = true;
                        $scope.IsTableVisible = true;
                        $scope.IsExcelButton = true;
                        $scope.NoRecLabel = false;
                        $scope.PaperWiseInwardReportTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        })

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
        var ExcelFileName = "PaperWiseInwardStatisticsReport" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:20px;font-weight:bold',
            caption: {
                title: 'PaperWiseInwardStatisticsReport|Date and Time: ' + '<br>' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
               
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'ProgrammeCode', title: 'ProgrammeCode' },
                { columnid: 'ProgrammeModeName', title: 'Programme Mode Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'PartTermName', title: 'Part Term Name' },
                { columnid: 'TeachingLearningMethodName', title: 'Teaching Learning Method Name' },
                { columnid: 'AssessmentType', title: 'Assessment Type' },
                { columnid: 'AssessmentMethodName', title: 'Assessment Method Name' },
                { columnid: 'PaperName', title: 'Paper Name' },
                { columnid: 'PaperCode', title: 'Paper Code' },
                { columnid: 'ExamDate', title: 'Exam Date' },
                { columnid: 'Duration', title: 'Duration' },
                { columnid: 'StudentCount', title: 'Student Count' },






            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.PaperWiseInwardReport]);
    };

});