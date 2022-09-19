app.directive("scheduleSelect", function () {
    return {
        templateUrl: "UI/components/scheduleSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.FacultyExamMapListGetActiveList = [];
            $scope.defaultScheduleList = [];
            var scheduleObj = {
                Id : 0,
                ScheduleName: "Select Schedule"
            }
            $scope.defaultScheduleList.push(scheduleObj);

            $scope.getFacultyExamMapListGetActive = function () {
            
                $http({
                    method: 'POST',
                    url: 'api/FacultyExamMap/FacultyExamMapListGetActive',
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
                          
                            $scope.FacultyExamMapListGetActiveList = $scope.defaultScheduleList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                   
                        alert(res.obj);
                    });
            }
            $scope.getFacultyExamMapListGetActive();


        }],
        link: function (e, t, a) {
        }
    }
})