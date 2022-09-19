app.controller('ExamBlocksCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Exam Blocks";

    $rootScope.showLoading = false;

      // for edit Exam Venue
    $scope.editExamBlock = function (data) {
        
        $scope.block = data;
  
        $scope.getExamVenueListGetActive($scope.block.ExamCenterId);
        $(window).scrollTop(0);
    };

    $scope.cancelExamBlock = function () {
        $scope.block = {
            ExamCenterId: 0,
            ExamVenueId:0
        };
    };

    $scope.cancelExamBlock();

    $scope.getExamBlocksListGet = function () {
        $rootScope.showLoading = true;
        
        var xml = new Object();
       
        $http({
            method: 'POST',           
            url: 'api/ExamBlocks/ExamBlocksListGetByInstId',            
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.ExamBlocksListGetList = response.obj;

                    var data = $scope.ExamBlocksListGetList.slice();

                    $scope.blockTableParams = new NgTableParams({
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
    $scope.getExamBlocksListGet();

    // for save
    $scope.saveExamBlocksAdd = function (data) {
        $rootScope.showLoading = true;
        $scope.block = data;
        $http({
            method: 'POST',
            url: 'api/ExamBlocks/ExamBlocksAdd',
            data: $scope.block,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.cancelExamBlock();
                    $scope.getExamBlocksListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for edit with api 
    $scope.editExamBlocksEdit = function (data) {
        
        $rootScope.showLoading = true;
        $scope.venue = data;
        $http({
            method: 'POST',
            url: 'api/ExamBlocks/ExamBlocksEdit',
            data: $scope.venue,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    /*    $scope.cancelExamBlock();*/
                    $scope.getExamBlocksListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save examblock with condition
    $scope.saveExamBlock = function (data) {

        if (data.Id === undefined || data.Id === null || data.Id === '') {
  
            $scope.saveExamBlocksAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
   
            $scope.editExamBlocksEdit(data);
        }

    }


    $scope.hideExamBlock = function (data) {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/ExamBlocks/ExamBlocksIsActive',
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
                    $scope.getExamBlocksListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    $scope.showExamBlock = function (data) {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/ExamBlocks/ExamBlocksIsInactive',
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
                    $scope.getExamBlocksListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };



});