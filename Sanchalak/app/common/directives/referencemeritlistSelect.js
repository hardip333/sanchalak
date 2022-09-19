app.directive("referencemeritlistSelect", function () {
    return {
        templateUrl: "UI/components/referencemeritlistSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.activeMeritListInstanceGetActive = [];
            $scope.defaultMeritList = [];
            var redrenceObj = {
                Id: 0,
                Name  : "Select Reference Merit List"
            }
            $scope.defaultMeritList.push(redrenceObj);

            $scope.getActiveMeritListInstanceGetActive = function (data) {
             
                if (data.ProgrammeInstancePartTermId !== null && data.AcademicYearId !== null && data.Round !== null) {
                    $http({
                        method: 'POST',
                        url: 'api/MeritListInstance/MeritListInstanceGetActive',
                        data: { ProgrammeInstancePartTermId: data.ProgrammeInstancePartTermId, AcademicYearId: data.AcademicYearId, Round: data.Round },
                        headers: { "Content-Type": 'application/json' }
                    })
                        .success(function (response) {
                            $rootScope.showLoading = false;

                            if (response.response_code != "200") {
                                alert(response.obj);
                            }
                            else {

                                $scope.tempList = response.obj;
                                $scope.activeMeritListInstanceGetActive = $scope.defaultMeritList.concat($scope.tempList);
                            
                            }
                        })
                        .error(function (res) {
                            alert(res.obj);
                        });
                }
                /*            $scope.getActiveMeritListInstanceGetActive();*/
            }

        }],
        link: function (e, t, a) {
        }
    }
})