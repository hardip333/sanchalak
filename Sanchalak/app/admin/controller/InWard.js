app.controller('InWardCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Inward";

    $rootScope.showLoading = false;
    $scope.IsInstitute = false;
    $scope.IsSection = false;
    $scope.showForm = false;
    $scope.cancelInward = function () {
        $scope.filter = {
            ExamMasterId: 0,
        };
    };

    $scope.cancelInward();
    $scope.GetFlagdata = function () {
       
        $scope.getpendingGetPendingExamInwardList();
    }
    // for get inward table 

    $scope.getpendingGetPendingExamInwardList = function () {

        $rootScope.showLoading = true;
        

        
        //var xml = new Object();
        //xml.ProgrammeId = $scope.filter.ProgrammeId;
        //xml.FacultyExamMapId = $scope.selectedSchedule.Id;
        //xml.ExamMasterId = $scope.selectedSchedule.ExamMasterId;

        $http({
            method: 'POST',
            url: 'api/ExamFormMaster/GetPendingExamInwardList',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.GetPendingExamInwardListlist = response.obj;

                    $scope.inwardTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: $scope.GetPendingExamInwardListlist
                    });



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
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
    $scope.SetInward = function (data) {
        
        if ($scope.IsInstitute == true) {
            if (data.IsFeeOpenForFaculty == true) {
                $scope.InwardData = data;
                $scope.showForm = true;
            }
            else { alert("kindly contact exam/academic section"); }
        }
        else {
            $scope.InwardData = data;
            $scope.showForm = true;
        }
        
    }
    // for update button in table 
    $scope.saveupdateExamInward = function (data) {

        $rootScope.showLoading = true;
        debugger;
        $http({
            method: 'POST',
            url: 'api/ExamFormMaster/updateExamInward',
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
                   
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
});