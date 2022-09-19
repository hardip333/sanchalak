app.directive("semesterSelect", function () {
    return {
        templateUrl: "UI/components/semesterSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.FacultyExamMapListGetActiveList = [];
            $scope.defaultSemesterList = [];
            var schedulesObj = {
                Id : 0,
                name : "Select Semester"
            }
            $scope.defaultSemesterList.push(schedulesObj);

            $scope.getFacultyExamMapListGetActive = function (data) {

        /*        alert(501);*/
                $http({
                    method: 'POST',
    /*              url: 'api/FacultyExamMap/MstFacultyExamMapGetActive',*/
                    url: 'api/FacultyExamMap/FacultyExamMapListGetActive',
                    data: { ProgrammeId: data},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                   
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                     
                            alert(response.obj);
                        }
                        else {
                        
                            $scope.tempList = response.obj;
                          
                            $scope.FacultyExamMapListGetActiveList = $scope.defaultSemesterList.concat($scope.tempList);
                            //alert(response.obj.StartDateOfExam);
                   
                            //alert(response.obj.EndDateOfExam);

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