app.controller('ExamEventCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Exam Event";

    $rootScope.showLoading = false;

    // for edit Exam Event 
    $scope.editExamEvent = function (data) {
        //debugger
        $scope.Examevent = data;

        //$scope.getExamMasterListGet();

        //if ($scope.Examevent.IsOnline === true) {
        //    $scope.Examevent.IsOnline = "1";
        //}
        //else {
        //    $scope.Examevent.IsOnline = "0";
        //}


    }

    $scope.cancelExamEvent = function () {
        $scope.Examevent = {
            AcademicYearId: 0
        };
    };

    $scope.cancelExamEvent();

    $scope.getExamMasterListGet = function () {
        $rootScope.showLoading = true;

        var xml = new Object();

        $http({
            method: 'POST',
            /*            url: 'api/ExamMaster/MstExamGet',*/
            url: 'api/ExamEventMaster/ExamEventMasterListGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    //debugger
                    $scope.ExamMasterListGetList = response.obj;



                    var len = $scope.ExamMasterListGetList.length;

                    for (var i = 0; i < len; i++) {

                        if ($scope.ExamMasterListGetList[i].Month === 1) {
                            $scope.ExamMasterListGetList[i].MonthDisplay = "Jan";
                        }
                        else if ($scope.ExamMasterListGetList[i].Month === 2) {
                            $scope.ExamMasterListGetList[i].MonthDisplay = "Feb";
                        }
                        else if ($scope.ExamMasterListGetList[i].Month === 3) {
                            $scope.ExamMasterListGetList[i].MonthDisplay = "Mar";
                        }
                        else if ($scope.ExamMasterListGetList[i].Month === 4) {
                            $scope.ExamMasterListGetList[i].MonthDisplay = "Apr";
                        }
                        else if ($scope.ExamMasterListGetList[i].Month === 5) {
                            $scope.ExamMasterListGetList[i].MonthDisplay = "May";
                        }
                        else if ($scope.ExamMasterListGetList[i].Month === 6) {
                            $scope.ExamMasterListGetList[i].MonthDisplay = "Jun";
                        }
                        else if ($scope.ExamMasterListGetList[i].Month === 7) {
                            $scope.ExamMasterListGetList[i].MonthDisplay = "Jul";
                        }
                        else if ($scope.ExamMasterListGetList[i].Month === 8) {
                            $scope.ExamMasterListGetList[i].MonthDisplay = "Aug";
                        }
                        else if ($scope.ExamMasterListGetList[i].Month === 9) {
                            $scope.ExamMasterListGetList[i].MonthDisplay = "Sep";
                        }
                        else if ($scope.ExamMasterListGetList[i].Month === 10) {
                            $scope.ExamMasterListGetList[i].MonthDisplay = "Oct";
                        }
                        else if ($scope.ExamMasterListGetList[i].Month === 11) {
                            $scope.ExamMasterListGetList[i].MonthDisplay = "Nov";
                        }
                        else if ($scope.ExamMasterListGetList[i].Month === 12) {
                            $scope.ExamMasterListGetList[i].MonthDisplay = "Dec";
                        }

                    }

                    /*                    $scope.classMasterCount = $scope.classMasterList.length;*/

                    var data = $scope.ExamMasterListGetList.slice();

                    $scope.ExameventTableParams = new NgTableParams({
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
    $scope.getExamMasterListGet();

    // for save
    $scope.saveExamMasterAdd = function (data) {
        $rootScope.showLoading = true;
        $scope.Examevent = data;

        $http({
            method: 'POST',
            url: 'api/ExamEventMaster/ExamEventMasterAdd',
            data: $scope.Examevent,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.cancelExamEvent();
                    $scope.getExamMasterListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for edit with api 
    $scope.editExamMasterEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.Examevent = data;

        $http({
            method: 'POST',
            url: 'api/ExamEventMaster/ExamEventMasterEdit',
            data: $scope.Examevent,
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
                    $scope.getExamMasterListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save saveExamevent with condition
    $scope.saveExamEvent = function (data) {

        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.saveExamMasterAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editExamMasterEdit(data);

        }

    }




    $scope.hideExamEvent = function (data) {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/ExamEventMaster/ExamEventMasterIsActive ',
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
                    $scope.getExamMasterListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    $scope.showExamEvent = function (data) {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/ExamEventMaster/ExamEventMasterIsInactive ',
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
                    $scope.getExamMasterListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };


});