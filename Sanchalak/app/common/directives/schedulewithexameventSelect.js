app.directive("schedulewithexameventSelect", function () {
    return {
        templateUrl: "UI/components/schedulewithexameventSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.FacultyExamMapListGetActiveList = [];
            $scope.defaultSchedulesList = [];
            var schedulesObj = {
                Id : 0,
                ScheduleCode: "Select Schedule"
            }
            $scope.defaultSchedulesList.push(schedulesObj);

            $scope.getFacultyExamMapListGetActive = function (data) {
            
                $http({
                    method: 'POST',
    /*              url: 'api/FacultyExamMap/MstFacultyExamMapGetActive',*/
                    url: 'api/FacultyExamMap/FacultyExamMapListGetActive',
                    data: { ExamMasterId: data},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                   
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                     
                            alert(response.obj);
                        }
                        else {
                        
                            $scope.tempList = response.obj;
                          
                            $scope.FacultyExamMapListGetActiveList = $scope.defaultSchedulesList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                   
                        alert(res.obj);
                    });
            }
      /*      $scope.getFacultyExamMapListGetActive();*/


        }],
        link: function (e, t, a) {
        }
    }
})