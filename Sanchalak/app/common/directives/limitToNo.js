app.directive("limitToNo", [function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            var limit = parseInt(attrs.limitToNo);
            angular.element(elem).on("keypress", function (event) {
                if (event.keyCode > 47 && event.keyCode < 58) {
                    if (this.value.length >= limit || this.value.includes("*"))
                        return false;
                }
                else if (event.keyCode === 42) {
                    if (this.value.length >= 1)
                        return false;
                }
                else
                    return false;
            });
        }
    }
}]);