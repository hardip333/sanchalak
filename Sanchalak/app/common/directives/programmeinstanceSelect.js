app.directive("programmeinstanceSelect", function () {
    return {
        templateUrl: "UI/components/programmeinstanceSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.IncProgrammeInstanceGetActiveList = [];
            $scope.defaultInstanceList = [];
            var instanceObj = {
                Id : 0,
                InstanceName: "Select Programme Instance"
            }
            $scope.defaultInstanceList.push(instanceObj);

            $scope.IncProgrammeInstanceGetActive = function (data) {

                $rootScope.showLoading = true;

                $http({
                    method: 'POST',
                    url: 'api/MstProgramInstance/IncProgrammeInstanceGetActive',
                    data: { ProgrammeId: data },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;
                        if (response.response_code != "200") {
                      
                            alert(response.obj);
                        }
                        else {
                            $scope.tempList = response.obj;

                            $scope.IncProgrammeInstanceGetActiveList = $scope.defaultInstanceList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                        $rootScope.showLoading = false;
                        alert(res.obj);
                    });
            }
    /*        $scope.IncProgrammeInstanceGetActive();*/
        }],
        link: function (e, t, a) {
        }
    }
})