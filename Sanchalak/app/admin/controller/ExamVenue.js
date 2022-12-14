app.controller('ExamVenueCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Exam Venue";

    $rootScope.showLoading = false;

      // for edit Exam Venue
    $scope.editExamVenue = function (data) {
        $scope.venue = data;
    };

    $scope.cancelExamVenue = function () {
        $scope.venue = {
            ExamCenterId: 0
        };
    };

    $scope.cancelExamVenue();

    $scope.getExamVenueListGet = function () {
        $rootScope.showLoading = true;

        var xml = new Object();

        $http({
            method: 'POST',
            url: 'api/ExamVenue/ExamVenueListGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.ExamVenueListGetList = response.obj;

                    /*  $scope.classMasterCount = $scope.classMasterList.length;*/

                    var data = $scope.ExamVenueListGetList.slice();

                    $scope.venueTableParams = new NgTableParams({
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
    $scope.getExamVenueListGet();

    // for save
    $scope.saveExamVenueAdd = function (data) {
        $rootScope.showLoading = true;
        $scope.venue = data;
        $http({
            method: 'POST',
            url: 'api/ExamVenue/ExamVenueAdd',
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
                    $scope.cancelExamVenue();
                    $scope.getExamVenueListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for edit with api 
    $scope.editExamVenueEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.venue = data;
        $http({
            method: 'POST',
            url: 'api/ExamVenue/ExamVenueEdit',
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
                    /*    $scope.cancelMstExam();*/
                    $scope.getExamVenueListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save examcenter with condition
    $scope.saveExamVenue = function (data) {

        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.saveExamVenueAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editExamVenueEdit(data);

        }

    }


    $scope.hideExamVenue = function (data) {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/ExamVenue/ExamVenueIsActive',
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
                    $scope.getExamVenueListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    $scope.showExamVenue = function (data) {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/ExamVenue/ExamVenueIsInactive',
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
                    $scope.getExamVenueListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };



});