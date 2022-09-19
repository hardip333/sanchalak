app.directive("coursewithschedulemapSelect", function () {
    return {
        templateUrl: "UI/components/coursewithschedulemapSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.CourseScheduleMapListGetActiveList = [];
            $scope.defaultPromapList = [];
            var coursesmapObj = {
                Id : 0,
                ProgrammeName: "Select Course"
            }
            $scope.defaultPromapList.push(coursesmapObj);

            $scope.getCourseScheduleMapListGetActive = function (data) {

                $rootScope.showLoading = true;

                $http({
                    method: 'POST',
           /*         url: 'api/CourseScheduleMap/MstCourseScheduleMapGetActive',*/
                    url: 'api/CourseScheduleMap/CourseScheduleMapListGetActive',
                    data: { FacultyExamMapId : data},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;
                        if (response.response_code != "200") {
                      
                            alert(response.obj);
                        }
                        else {
                            $scope.tempList = response.obj;
                            $scope.CourseScheduleMapListGetActiveList = $scope.defaultPromapList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        $rootScope.showLoading = false;
                        alert(res.obj);
                    });
            }
     /*       $scope.MstProgrammeMasterGet();*/
        }],
        link: function (e, t, a) {
        }
    }
})