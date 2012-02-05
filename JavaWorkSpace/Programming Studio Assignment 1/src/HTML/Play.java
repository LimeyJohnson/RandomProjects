/*A play represents a One Game by One user, Any game may have many plays, and any user may have many plays. 
 * However, a Play can only have one user and one game.
 */
package HTML;
import java.util.*;

public class Play {
	User user;
	Game game;
	Date date;
	public Play(User u, Game g, Date d){
		user = u; game = g; date = d;
	}
}
