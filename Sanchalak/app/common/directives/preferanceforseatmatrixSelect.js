app.directive("preferanceforseatmatrixSelect", function () {
    return {
        templateUrl: "UI/components/preferanceforseatmatrixSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.activeMstPreferenceGroupGetByProgInstPartTermIdList = [];
            $scope.defaultPreferanceList = [];
            var preObj = {
               Id: 0,
               GroupName: "Select Preferance"
            }
            $scope.defaultPreferanceList.push(preObj);
            $scope.getActiveMstPreferenceGroupGetByProgInstPartTermIdList = function () {
                $scope.ProgrammeInstancePartTermId = $rootScope.ProgrammeInstancePartTermId;
                $http({
                    method: 'POST',
                    url: 'api/MstPreferenceGroup/MstPreferenceGroupGetByProgInstPartTermId',
                    data: { ProgrammeInstancePartTermId},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {
                            $scope.tempList = response.obj;
                            $scope.activeMstPreferenceGroupGetByProgInstPartTermIdList = $scope.defaultPreferanceList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getActiveMstPreferenceGroupGetByProgInstPartTermIdList();


        }],
        link: function (e, t, a) {
        }
    }
})