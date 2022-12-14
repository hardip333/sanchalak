app.controller('ApplicationConfigurationByAcademicCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Application Configuration for Academic Section";

    $scope.ConfigDate = {};
    $localStorage.PTConfig = {}; 
    $scope.ShowTableFlag = false;
    $scope.HideOtherSection = false;
    $scope.BackFlag = false;

    $scope.getMstInstitute = function () {
        //$scope.ShowTableFlag = false;
        $scope.InstituteList = {};
        $scope.ProgrammeList = {};
        $scope.ProgPartList = {};
        $scope.BList = {};
        $scope.PTList = {};

        $http({
            method: 'POST',
            url: 'api/ApplicationConfigurationByAcademic/MstInstituteGet',
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
                        $scope.InstituteList = {};

                    }
                }
                else {
                    $scope.InstituteList = response.obj;

                }
            })
       
            .error(function (res) {
                alert(res);
            });
    };

    $scope.IncAcademicYearListGet = function () {
       
        //$scope.ShowTableFlag = false;
        $scope.AcademicList = {};
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/AcademicYearGet',
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
                        $scope.AcademicList = {};

                    }
                }
                else {
                    $scope.AcademicList = response.obj;
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    /* Programme List Get Method*/
    $scope.ProgrammeGetbyInstId = function () {

        //$scope.ShowTableFlag = false;
        $scope.ProgrammeList = {};
        $http({
            method: 'POST',
            url: 'api/ApplicationConfigurationByAcademic/MstProgrammeGetByInstId',
            data: $scope.ConfigDate,
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
                        $scope.ProgrammeList = {};

                    }
                }
                else {
                    $scope.ProgrammeList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.ProgPartInstListGet = function () {
        //$scope.ShowTableFlag = false;
        $scope.ProgPartList = {};
        $http({
            method: 'POST',
            url: 'api/ApplicationConfigurationByAcademic/ProgrammePartListGetByProgrammeId',
            data: $scope.ConfigDate,
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
                        $scope.ProgPartList = {};

                    }
                }
                else {
                    $scope.ProgPartList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /* Branch List Get Method*/
    $scope.BranchGet = function () {
        //$scope.ShowTableFlag = false;
        $scope.BList = {};
        $http({
            method: 'POST',
            url: 'api/ApplicationConfigurationByAcademic/MstProgrammeBranchListGetByProgrammePartId',
            data: $scope.ConfigDate,
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
                        $scope.BList = {};

                    }
                }
                else {
                    $scope.BList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /* ProgrammePT List Get Method*/
    $scope.PTsGet = function () {
        //$scope.ShowTableFlag = false;
        $scope.PTList = {};
        $http({
            method: 'POST',
            url: 'api/ApplicationConfigurationByAcademic/ProgrammePartTermListGetByProgrammePartId',
            data: $scope.ConfigDate,
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
                        $scope.PTList = {};
                        $scope.ShowInstitute = false;
                        $scope.ShowTable = false;
                        //alert(response.obj);
                    }
                }
                else {
                    $scope.PTList = response.obj;
                    $localStorage.PTConfig = {};
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.GetConfiguredProgramme = function () {  
        //alert($localStorage.PTConfig.ProgrammeInstancePartTermId);
        if ($localStorage.PTConfig.ProgrammeInstancePartTermId == null || $localStorage.PTConfig.ProgrammeInstancePartTermId == undefined || $localStorage.PTConfig.ProgrammeInstancePartTermId == "") {
            $localStorage.PTConfig = {};
            $localStorage.PTConfig.ProgrammeInstancePartTermId = $scope.ConfigDate.ProgrammeInstancePartTerm.Id;

        }
  
        $http({
            method: 'POST',
            url: 'api/ApplicationConfigurationByAcademic/AdmApplicationConfigurationGetById',
            data: { ProgrammeInstancePartTermId: $localStorage.PTConfig.ProgrammeInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        //$scope.GetApplicationFeeDetail();
                        $scope.AppConfigList = response.obj;
                        //$scope.SequenceCountFlag = $scope.AppConfigList[0].SequenceCount;
                        $scope.ShowTableFlag = true;
                        $scope.checkDataExists = false;
                        debugger
                        if (response.obj == "No Record Found" && $scope.BackFlag == false) {

                            $scope.showFormFlag = true; // Add for Insertion by Mohini on 06-Sep-2022
                            $scope.HideOtherSection = true; // Add for Insertion by Mohini on 06-Sep-2022
                            //$scope.SequenceCountFlag = true; // Add for Insertion by Mohini on 06-Sep-2022
                            $scope.BackFlag = true; // Add for Insertion by Mohini on 06-Sep-2022

                            $scope.checkDataExists = true;
                            $scope.ShowTableFlag = false;
                            $scope.ConfigDate = {};

                        }

                        if (response.obj == "No Record Found" && $scope.BackFlag == true) {
                            $scope.checkDataExists = true;
                            $scope.ShowTableFlag = false;
                            $scope.ConfigDate = {};
                            $scope.getMstInstitute();
                        }
                        
                        //  console.log($scope.GroupList);
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.displayAppConfig = function (data) {
        $scope.ConfigDate = data;
        $scope.AcademicList = {};
        $scope.InstituteList = {};
        $scope.ProgrammeList = {};
        $scope.ProgPartList = {};
        $scope.BList = {};
        $scope.PTList = {};
        $scope.IncAcademicYearListGet();
    };

    $scope.modifyApplicationDateConfigData = function (data) {

        $scope.showFormFlag = true;
        $scope.ConfigDate = data;
        $scope.HideOtherSection = true;
        //$scope.GetConfiguredProgramme();
       

        var appStartDate = $scope.ConfigDate.ApplicationStartDateView.split("-");
        $scope.ConfigDate.ApplicationStartDate = new Date(appStartDate[2], (appStartDate[1] >= 1) ? (appStartDate[1] - 1) : appStartDate[1], appStartDate[0]);

        var appEndDate = $scope.ConfigDate.ApplicationStopDateView.split("-");
        $scope.ConfigDate.ApplicationStopDate = new Date(appEndDate[2], (appEndDate[1] >= 1) ? (appEndDate[1] - 1) : appEndDate[1], appEndDate[0]);

        var appFeeStartDate = $scope.ConfigDate.ApplicationFeeStartDateView.split("-");
        $scope.ConfigDate.ApplicationFeeStartDate = new Date(appFeeStartDate[2], (appFeeStartDate[1] >= 1) ? (appFeeStartDate[1] - 1) : appFeeStartDate[1], appFeeStartDate[0]);

        var appFeeEndDate = $scope.ConfigDate.ApplicationFeeEndDateView.split("-");
        $scope.ConfigDate.ApplicationFeeEndDate = new Date(appFeeEndDate[2], (appFeeEndDate[1] >= 1) ? (appFeeEndDate[1] - 1) : appFeeEndDate[1], appFeeEndDate[0]);

        var admStartDate = $scope.ConfigDate.AdmissionFeesStartDateView.split("-");
        $scope.ConfigDate.AdmissionFeesStartDate = new Date(admStartDate[2], (admStartDate[1] >= 1) ? (admStartDate[1] - 1) : admStartDate[1], admStartDate[0]);

        var admEndDate = $scope.ConfigDate.AdmissionFeesStopDateView.split("-");
        $scope.ConfigDate.AdmissionFeesStopDate = new Date(admEndDate[2], (admEndDate[1] >= 1) ? (admEndDate[1] - 1) : admEndDate[1], admEndDate[0]);

       
        if ($scope.ConfigDate.AdmInstallementEndDateView == null || $scope.ConfigDate.AdmInstallementEndDateView == undefined || $scope.ConfigDate.AdmInstallementEndDateView == "") {
            $scope.IsVisibleBtn = false;

        }

        else {
            var admInstallementEndDate = $scope.ConfigDate.AdmInstallementEndDateView.split("-");
            $scope.ConfigDate.AdmInstallementEndDate = new Date(admInstallementEndDate[2], (admInstallementEndDate[1] >= 1) ? (admInstallementEndDate[1] - 1) : admInstallementEndDate[1], admInstallementEndDate[0]);
            $scope.IsVisibleBtn = true;

        }
    };

    $scope.modifyApplicationDateConfig = function () {

        $scope.ConfigDate.ProgramInstancePartTermId = $localStorage.PTConfig.ProgrammeInstancePartTermId;

        if ($scope.ConfigDate.ApplicationStartDate == undefined || $scope.ConfigDate.ApplicationStartDate == null) {
            alert("Please select Application Start Date.");
        }
        else if ($scope.ConfigDate.ApplicationStopDate == undefined || $scope.ConfigDate.ApplicationStopDate == null) {
            alert("Please select Application Stop Date.");
        }
        else if (($scope.ConfigDate.ApplicationStopDate != 'Invalid Date') && ($scope.ConfigDate.ApplicationStartDate == 'Invalid Date')) {
            alert("Please select Application Start Date.");
        }
        else if (($scope.ConfigDate.ApplicationStartDate != 'Invalid Date') && ($scope.ConfigDate.ApplicationStopDate == 'Invalid Date')) {
            alert("Please select Application End Date.");
        }

        else if (($scope.ConfigDate.ApplicationFeeEndDate != 'Invalid Date') && ($scope.ConfigDate.ApplicationFeeStartDate == 'Invalid Date')) {
            alert("Please select Application-Fees Start Date.");
        }
        else if (($scope.ConfigDate.ApplicationFeeStartDate != 'Invalid Date') && ($scope.ConfigDate.ApplicationFeeEndDate == 'Invalid Date')) {
            alert("Please select Application-Fees End Date.");
        }

        else if (($scope.ConfigDate.AdmissionFeesStopDate != 'Invalid Date') && ($scope.ConfigDate.AdmissionFeesStartDate == 'Invalid Date')) {
            alert("Please select Admission-Fees Start Date.");
        }
        else if (($scope.ConfigDate.AdmissionFeesStartDate != 'Invalid Date') && ($scope.ConfigDate.AdmissionFeesStopDate == 'Invalid Date')) {
            alert("Please select Admission-Fees End Date.");
        }

        else if ($scope.ConfigDate.ApplicationStartDate > $scope.ConfigDate.ApplicationStopDate) {
            alert("Application Start Date should not be Greater than Application End Date.");
        }
        else if ($scope.ConfigDate.ApplicationFeeStartDate > $scope.ConfigDate.ApplicationFeeEndDate) {
            alert("Application-Fee Start Date should not be Greater than Application-Fee End Date.");
        }
        else if ($scope.ConfigDate.AdmissionFeesStartDate > $scope.ConfigDate.AdmissionFeesStopDate) {
            alert("Admission-Fees Start Date should not be Greater than Admission-Fees End Date.");
        }
      
        else if (($scope.ConfigDate.ApplicationFeeStartDate != 'Invalid Date' && $scope.ConfigDate.ApplicationFeeStartDate != null) &&
            ($scope.ConfigDate.ApplicationStartDate > $scope.ConfigDate.ApplicationFeeStartDate)) {
            alert("Application Start Date should not be Greater than Application Fee Start Date.");
        }
        else if (($scope.ConfigDate.ApplicationApprovalStartDate != 'Invalid Date' && $scope.ConfigDate.ApplicationApprovalStartDate != null) &&
            ($scope.ConfigDate.ApplicationStartDate > $scope.ConfigDate.ApplicationApprovalStartDate)) {
            alert("Application Start Date should not be Greater than Application Approval Start Date.");
        }
        //else if (($scope.ConfigDate.IsPaperSelByStudent == null || $scope.ConfigDate.IsPaperSelByStudent == undefined)) {
        //    alert("Please select Yes or No option for Paper Selection By Student.");
        //}
        //else if (($scope.ConfigDate.IsPaperSelBeforeFees == null || $scope.ConfigDate.IsPaperSelBeforeFees == undefined)) {
        //    alert("Please select Yes or No option for Paper Selection Before Fees.");
        //}

        else {

            $http({
                method: 'POST',
                url: 'api/ApplicationConfigurationByAcademic/updateAdmApplicationConfiguration',
                data: $scope.ConfigDate,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        alert(response.obj);
                        $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                    } else {
                        alert(response.obj);
                        $scope.showFormFlag = false;
                        $scope.HideOtherSection = false;
                        $scope.AcademicList = {};
                        $scope.InstituteList = {};
                        $scope.ProgrammeList = {};
                        $scope.ProgPartList = {};
                        $scope.BList = {};
                        $scope.PTList = {};
                        $scope.GetConfiguredProgramme();
                        $scope.IncAcademicYearListGet();
                        //$scope.getPreApplicationConfigById();
                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.backToAppConfigByAcademic = function (data) {
        
        $scope.showFormFlag = false;
        $scope.HideOtherSection = false;
        $scope.AcademicList = {};
        $scope.InstituteList = {};
        $scope.ProgrammeList = {};
        $scope.ProgPartList = {};
        $scope.BList = {};
        $scope.PTList = {};
        $scope.GetConfiguredProgramme();
        $scope.IncAcademicYearListGet();
        $state.go('ApplicationConfigurationByAcademic');
        //$scope.IncAcademicYearListGet();
        //$scope.GetConfiguredProgramme();

    };

    $scope.RemoveInstallementDate = function () {

        $scope.AppConfig.AdmInstallementEndDate = null;
        $scope.IsVisibleBtn = false;
        $scope.modifyApplicationDateConfig();

    }
    
})