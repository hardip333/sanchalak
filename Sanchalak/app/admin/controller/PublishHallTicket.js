app.controller('PublishHallTicketCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Publish Hall Ticket";
    $scope.CourseListTableParams = new NgTableParams({
    }, {
        dataset: []
    });
    $scope.PHT = {};
    $scope.ShowEditBlock = false;


    $scope.GetCourse = function () {

        $scope.CourseListTableParams = new NgTableParams({
        }, {
            dataset: []
        });
        $http({
            method: 'POST',
            url: 'api/PublishHallTicket/FacultyExamMapStatusGetById',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                    $scope.CourseListTableParams = new NgTableParams({
                    }, {
                        dataset: []
                    });
                }
                else {

                    $scope.CourseList = response.obj;

                    console.log($scope.CourseList);
                    debugger;
                    for (var i in $scope.CourseList) {
                            if ($scope.CourseList[i].DyrExamSignDisplay != "Not Defined") {
                                $scope.CourseList[i].ShowButton = true;
                                $scope.CourseList[i].ErrMsg = "";
                            }
                            else { $scope.CourseList[i].ShowButton = false; $scope.CourseList[i].ErrMsg = "Signature not attached"; }
                       
                    }
                    $scope.CourseListTableParams = new NgTableParams({
                    }, {
                        dataset: $scope.CourseList
                    });


                }

            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
    $scope.ShowMode = function (course) {
        $scope.PHT = {};
        $scope.ShowEditBlock = false;
        if (course.ShowButton == false) {
            alert(course.ErrMsg);
            $scope.PHT = {};
            $scope.ShowEditBlock = false;

        }
        else {
            $scope.PHT = course;
            $scope.ShowEditBlock = true;
        }
    };
    $scope.GoEdit = function () {
        $state.go('HallticketInstructionConfiguration');
    };
    
    $scope.PublishHallTicket = function () {
        $http({
            method: 'POST',
            url: 'api/PublishHallTicket/PublishHallTicketTrue',
            data: $scope.PHT,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {

                    alert(response.obj);
                    $scope.PHT = {};
                    $scope.ShowEditBlock = false;
                    $scope.GetCourse();
                }

            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
});