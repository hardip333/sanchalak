app.directive("addonquestionsSelect", function () {
    return {
        templateUrl: "UI/components/addonquestionsSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.activeAdmProgrammeAddOnCriteriaGetAll = [];
            $scope.defaultAddonList = [];
            var addonObj = {
                Id: 0,
                TitleName: "Select Add On-Question"
            }
            $scope.defaultAddonList.push(addonObj);

            $scope.getActiveAdmProgrammeAddOnCriteriaGetAll = function () {

                $http({
                    method: 'POST',
                    url: 'api/AdmProgrammeAddOnCriteria/AdmProgrammeAddOnCriteriaGetAll',
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
                            $scope.activeAdmProgrammeAddOnCriteriaGetAll = $scope.defaultAddonList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getActiveAdmProgrammeAddOnCriteriaGetAll();


        }],
        link: function (e, t, a) {
        }
    }
})