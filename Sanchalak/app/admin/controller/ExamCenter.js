app.controller('ExamCenterCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Exam Center";

       $rootScope.showLoading = false;

    // for edit Exam Center
    $scope.editExamCenter = function (data) {
        $scope.examcenter = data;
    }

    $scope.cancelExamCenter = function () {
        $scope.examcenter = {};
      
    };

    $scope.cancelExamCenter();

    $scope.getExamCenterListGet = function () {
        $rootScope.showLoading = true;
       
        var xml = new Object();

        $http({
            method: 'POST',
            url: 'api/ExamCenter/ExamCenterListGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
       
                   $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.ExamCenterListGetList = response.obj;
             
                    /*  $scope.classMasterCount = $scope.classMasterList.length;*/

                    var data = $scope.ExamCenterListGetList.slice();

                    $scope.examcenterTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: data
                    });
                }
            })
            .error(function (res) {
          
                  $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
    $scope.getExamCenterListGet();

    // for save
    $scope.saveExamCenterAdd = function (data) {
        $rootScope.showLoading = true;

        $scope.examcenter = data;
        $http({
            method: 'POST',
            url: 'api/ExamCenter/ExamCenterAdd',
            data: $scope.examcenter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
              
                }
                else {
                    alert(response.obj);
                    $scope.cancelExamCenter();
                    $scope.getExamCenterListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            /*    alert(res.obj);*/
            });
    };

    // for edit with api 
    $scope.editExamCenterEdit = function (data) {
    
        $rootScope.showLoading = true;
        $scope.examcenter = data;
        $http({
            method: 'POST',
            url: 'api/ExamCenter/ExamCenterEdit',
            data: $scope.examcenter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getExamCenterListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save examcenter with condition
    $scope.saveExamCenter = function (data) {
     
        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.saveExamCenterAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editExamCenterEdit(data);

        }

    }

    $scope.hideExamCenter = function (data) {
        $rootScope.showLoading = true;
   
        
        $http({
            method: 'POST',
            url: 'api/ExamCenter/ExamCenterIsActive',
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
                    $scope.getExamCenterListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });

          
    };

    $scope.showExamCenter = function (data) {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/ExamCenter/ExamCenterIsInactive',
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
                    $scope.getExamCenterListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
});