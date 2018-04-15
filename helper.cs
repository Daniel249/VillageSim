static class Helper {
    public static int[] IntegerDivision(int num, int div) {
        int[] ret = new int[div];

        for(int i = 0; i < div; i++) {
            int current = num / (div - i);
            ret[i] = current;

            num -= current;
        }
        return ret;
    }
}