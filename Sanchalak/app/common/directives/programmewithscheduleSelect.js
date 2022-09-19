app.directive("programmewithscheduleSelect", function () {
    return {
        templateUrl: "UI/components/programmewithscheduleSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstProgrammeMasterGetList = [];
            $scope.defaultProList = [];
            var coursesObj = {
                Id : 0,
                ProgrammeName: "Select Programme"
            }
            $scope.defaultProList.push(coursesObj);

            $scope.MstProgrammeMasterGet = function (data) {

                $rootScope.showLoading = true;

                $http({
                    method: 'POST',
                    url: 'api/MstProgramme/MstProgrammeMasterGet',
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
                            $scope.MstProgrammeMasterGetList = $scope.defaultProList.concat($scope.tempList);
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